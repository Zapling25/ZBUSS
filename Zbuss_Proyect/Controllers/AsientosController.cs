using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zbuss_Proyect.Models;

namespace Zbuss_Proyect.Controllers
{
    [Authorize()]
    public class AsientosController : Controller
    {
        private readonly bd_VENTAS_ZBUSSContext _context;

        public AsientosController(bd_VENTAS_ZBUSSContext context)
        {
            _context = context;
        }

        // GET: AsientosController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AsientosIndex(int? id)
        {
            var asientosBd = (from m in _context.TbAsientosBus
                             where m.Idbus == id
                             select m).Include(t => t.IdbusNavigation);
            
            return View(asientosBd);
        }

        // GET: AsientosController/Details/5
        public List<TbAsientosBus> SeleccionAsientos(int id)
        {
            var busBd = _context.TbAsientosBus.FirstOrDefault(m => m.Idbus == id);

            if (!AsientosExists(id))
            {
                ViewBag.Alert = "Al bus de este viaje aún no se le ha asignado asientos. Pruebe otro";
                return null;
            }

            var _Asientos = (from TbAsientosBus v in _context.TbAsientosBus
                  where v.Idbus == busBd.Idbus
                  select v).ToList();
            return _Asientos;
        }

        // GET: AsientosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AsientosController/Create
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

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asientoBd = _context.TbAsientosBus
                .Include(t => t.IdbusNavigation)
                .FirstOrDefault(m => m.Idasiento == id);
            if (asientoBd == null)
            {
                return NotFound();
            }

            return View(asientoBd);
        }

        // GET: AsientosController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asientoBd = _context.TbAsientosBus.Find(id);
            if (asientoBd == null)
            {
                return NotFound();
            }
            return View(asientoBd);
        }

        // POST: AsientosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TbAsientosBus pAsiento)
        {
            if (id != pAsiento.Idasiento)
            {
                return NotFound();
            }

            TbAsientosBus asientoBd = _context.TbAsientosBus.FirstOrDefault(x => x.Idasiento == pAsiento.Idasiento);

            if (asientoBd == null)
            {
                return BadRequest();
            }

            asientoBd.Inclinacion = pAsiento.Inclinacion;
            asientoBd.Precio = pAsiento.Precio;
            asientoBd.Estado = pAsiento.Estado;

            _context.Entry(asientoBd).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("AsientosIndex",new { id=asientoBd.Idbus});
        }

        // GET: AsientosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AsientosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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

        private bool AsientosExists(int idBus)
        {
            return _context.TbAsientosBus.Any(e => e.Idbus == idBus);
        }
    }
}
