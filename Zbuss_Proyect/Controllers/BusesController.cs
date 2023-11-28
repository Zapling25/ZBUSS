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
    public class BusesController : Controller
    {
        private readonly bd_VENTAS_ZBUSSContext _context;

        public BusesController(bd_VENTAS_ZBUSSContext context)
        {
            _context = context;
        }

        // GET: BusesController
        public ActionResult Index(string searchString)
        {
            var busesDb = from m in _context.TbBus
                          select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                busesDb = busesDb.Where(u => u.Placa.Contains(searchString));
            }

            return View(busesDb);
        }

        // GET: BusesController/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busDb = _context.TbBus
                .FirstOrDefault(m => m.Idbus == id);
            if (busDb == null)
            {
                return NotFound();
            }

            return View(busDb);
        }

        // GET: BusesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Idbus,Placa,Capacidad,Pisos")] TbBus pBus)
        {
            if (!TbBusExists(pBus.Placa))
            {
                TbBus busBd = new TbBus()
                {
                    Placa = pBus.Placa,
                    Capacidad = pBus.Capacidad,
                    Pisos = pBus.Pisos
                };
                if (ModelState.IsValid)
                {
                    _context.TbBus.Add(busBd);

                    _context.SaveChanges();

                    for (int i = 1; i <= pBus.Capacidad; i++)
                    {
                        TbAsientosBus asientoBd = new TbAsientosBus()
                        {
                            Idbus = busBd.Idbus,
                            CodAsiento = i,
                            Inclinacion = "120º",
                            Precio = 80,
                            PisoBus = 1,
                            Estado = true
                        };
                        _context.TbAsientosBus.Add(asientoBd);
                        _context.SaveChanges();
                    }
                    pBus.Idbus = busBd.Idbus;

                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.Alert = "No se puede crear. Ya existe un bus con la misma placa";
            return View();
            
        }

        // GET: BusesController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busDb = _context.TbBus.Find(id);
            if (busDb == null)
            {
                return NotFound();
            }
            return View(busDb);
        }

        // POST: BusesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Idbus,Placa,Capacidad,Pisos")] TbBus pBus)
        {
            if (id != pBus.Idbus)
            {
                return NotFound();
            }

            TbBus busDb = _context.TbBus.FirstOrDefault(x => x.Idbus == pBus.Idbus);

            if (busDb == null)
            {
                return BadRequest();
            }

            busDb.Placa = pBus.Placa;
            busDb.Capacidad = pBus.Capacidad;
            busDb.Pisos = pBus.Pisos;

            _context.Entry(busDb).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: BusesController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDetalleViaje = _context.TbBus
                .FirstOrDefault(m => m.Idbus == id);
            if (tbDetalleViaje == null)
            {
                return NotFound();
            }

            return View(tbDetalleViaje);
        }

        // POST: BusesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var busDb = _context.TbBus.Find(id);
                _context.TbBus.Remove(busDb);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Alert = "No se puede eliminar porque el bus está relacionado con otra tabla";
                return View();
            }
        }
        private bool TbBusExists(string placa)
        {
            return _context.TbBus.Any(e => e.Placa == placa);
        }
    }
}
