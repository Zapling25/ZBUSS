using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zbuss_Proyect.Models;

namespace Zbuss_Proyect.Controllers
{
    public class VentasController : Controller
    {
        private readonly bd_VENTAS_ZBUSSContext _context;

        public VentasController(bd_VENTAS_ZBUSSContext context)
        {
            _context = context;
        }

        // GET: VentasController
        public ActionResult Index()
        {
            var ventaBd = _context.TbDetalleVenta.Include(x=>x.IduserNavigation);
            return View(ventaBd.ToList());
        }

        public ActionResult VentasCanceladas()
        {
            var ventaBd = (from v in _context.TbDetalleVenta
                           where v.Estado == false select v)
                           .Include(x => x.IduserNavigation)
                           .Include(x => x.IdasientoNavigation)
                           .Include(x => x.IdusuarioNavigation);

            return View(ventaBd.ToList());
        }

        public ActionResult Reservas()
        {
            var correo = HttpContext.User.Identity.Name;
            var userDb = _context.TbUsuarios.FirstOrDefault(x => x.Correo == correo);

            if (userDb == null)
            {
                return View();
            }

            var ventasDb = (from m in _context.TbDetalleVenta
                           where m.Idcuenta == userDb.IdUsuario &&
                                 m.Estado == true
                          select m).Include(x=>x.IdasientoNavigation).Include(x=>x.IduserNavigation);

            if (ventasDb == null)
            {
                return View();
            }

            return View(ventasDb);
        }
        public ActionResult CancerlarReserva(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventaBd = _context.TbDetalleVenta
                           .Include(x => x.IduserNavigation)
                           .Include(x => x.IdusuarioNavigation)
                           .Include(x => x.IdasientoNavigation)
                .FirstOrDefault(x => x.Idventa == id);
            if (ventaBd == null)
            {
                return NotFound();
            }

            return View(ventaBd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CancerlarReserva(int id)
        {

            var reservaBd = _context.TbDetalleVenta
                .Include(x => x.IduserNavigation)
                .Include(x => x.IdusuarioNavigation)
                .Include(x => x.IdasientoNavigation)
                .FirstOrDefault(x => x.Idventa == id);
            if(reservaBd == null)
            {
                return BadRequest();
            }

            var reservaFecha = reservaBd.FechaViaje;
            var fechaHoy = DateTime.Now.Date;
            var fechaLimite = reservaFecha.AddDays(-10);

            if (fechaHoy >= fechaLimite)
            {
                ViewBag.Alert = "Ya no se puede cancelar porque el viaje es el mismo dia o ya pasó";
                return View(reservaBd);
            }
            else
            {
                TbAsientosBus asientoBd = _context.TbAsientosBus.FirstOrDefault(x => x.Idasiento == reservaBd.Idasiento);
                asientoBd.Estado = true;
                _context.Entry(asientoBd).State = EntityState.Modified;

                reservaBd.Estado = false;
                _context.Entry(reservaBd).State = EntityState.Modified;

                _context.SaveChanges();
                ViewBag.Success = "Cancelación exitosa";
            }

            return RedirectToAction(nameof(Reservas));
        }

        // GET: VentasController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventaDb = await _context.TbDetalleVenta
                .Include(t => t.IdasientoNavigation).Include(t => t.IduserNavigation)
                .FirstOrDefaultAsync(m => m.Idventa == id);
            if (ventaDb == null)
            {
                return NotFound();
            }

            return View(ventaDb);
        }

        // GET: VentasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VentasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VentasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VentasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VentasController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventaDb = await _context.TbDetalleVenta
                .Include(t => t.IdasientoNavigation).Include(t => t.IduserNavigation)
                .FirstOrDefaultAsync(m => m.Idventa == id);
            if (ventaDb == null)
            {
                return NotFound();
            }

            return View(ventaDb);
        }

        // POST: VentasController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ventaDb = await _context.TbDetalleVenta.FindAsync(id);
            _context.TbDetalleVenta.Remove(ventaDb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
