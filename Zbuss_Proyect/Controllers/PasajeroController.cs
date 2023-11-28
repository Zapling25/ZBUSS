using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zbuss_Proyect.Models;

namespace Zbuss_Proyect.Controllers
{
    public class PasajeroController : Controller
    {
        private readonly bd_VENTAS_ZBUSSContext _context;

        public PasajeroController(bd_VENTAS_ZBUSSContext context)
        {
            _context = context;
        }

        // GET: Pasajero
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbPasajero.ToListAsync());
        }

        // GET: Pasajero/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbPasajero = await _context.TbPasajero
                .FirstOrDefaultAsync(m => m.Iduser == id);
            if (tbPasajero == null)
            {
                return NotFound();
            }

            return View(tbPasajero);
        }

        //GET: Pasajero/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pasajero/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public TbPasajero Registrar(TbPasajero pPasa)
        {
            if (pPasa.TpoDocumento == null || pPasa.NroDocumento == null || pPasa.Nombre == null ||
                pPasa.ApePaterno == null || pPasa.ApeMaterno == null || pPasa.Correo == null ||
                pPasa.Celular == null || pPasa.Sexo == null || pPasa.FechaNac == null)
            {
                ViewBag.Alert = "Faltan completar datos. Porfavor llenar todos los campos";
                return null;
            }
            if (!TbPasajeroExists(pPasa.NroDocumento))
            {
                TbPasajero pasaBd = new TbPasajero()
                {
                    TpoDocumento = pPasa.TpoDocumento,
                    NroDocumento = pPasa.NroDocumento,
                    Nombre = pPasa.Nombre,
                    ApePaterno = pPasa.ApePaterno,
                    ApeMaterno = pPasa.ApeMaterno,
                    Correo = pPasa.Correo,
                    Celular = pPasa.Celular,
                    Sexo = pPasa.Sexo,
                    FechaNac = pPasa.FechaNac,
                    Estado = "ACTIVO"
                };
                if (ModelState.IsValid)
                {
                    _context.TbPasajero.Add(pasaBd);
                    _context.SaveChanges();

                    pPasa.Iduser = pasaBd.Iduser;

                    return pasaBd;
                }

                return null;
            }
            ViewBag.Alert = "El dni ingresado ya está asociado a un pasajero. Pruebe con otro";
            return null;
        }

        // GET: Pasajero/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbPasajero = await _context.TbPasajero.FindAsync(id);
            if (tbPasajero == null)
            {
                return NotFound();
            }
            return View(tbPasajero);
        }

        // POST: Pasajero/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Iduser,TpoDocumento,NroDocumento,Nombre,ApePaterno,ApeMaterno,Correo,Celular,Sexo,FechaNac,Estado")] TbPasajero tbPasajero)
        {
            if (id != tbPasajero.Iduser)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbPasajero);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbPasajeroExists(tbPasajero.NroDocumento))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Reservas","Ventas");
            }
            return View(tbPasajero);
        }

        // GET: Pasajero/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbPasajero = await _context.TbPasajero
                .FirstOrDefaultAsync(m => m.Iduser == id);
            if (tbPasajero == null)
            {
                return NotFound();
            }

            return View(tbPasajero);
        }

        // POST: Pasajero/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbPasajero = await _context.TbPasajero.FindAsync(id);
            _context.TbPasajero.Remove(tbPasajero);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbPasajeroExists(string dni)
        {
            return _context.TbPasajero.Any(e => e.NroDocumento == dni);
        }

    }
}
