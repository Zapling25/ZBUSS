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
    [Authorize()]
    public class ViajesController : Controller
    {
        private readonly bd_VENTAS_ZBUSSContext _context;

        public ViajesController(bd_VENTAS_ZBUSSContext context)
        {
            _context = context;
        }

        // GET: Viajes
        public IActionResult Index(string searchString)
        {
            var viajesBd = (from m in _context.TbDetalleViaje
                           select m).Include(t => t.IdbusNavigation);

            if (!String.IsNullOrEmpty(searchString))
            {
                viajesBd = viajesBd.Where(u => (u.PuntoPartida.Contains(searchString) || u.PuntoLlegada.Contains(searchString)))
                    .Include(t => t.IdbusNavigation);
            }

            return View(viajesBd);
            //return View(_context.TbDetalleViaje.ToList());
        }

        // GET: Viajes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDetalleViaje = await _context.TbDetalleViaje
                .Include(t => t.IdbusNavigation)
                .FirstOrDefaultAsync(m => m.Idviaje == id);
            if (tbDetalleViaje == null)
            {
                return NotFound();
            }

            return View(tbDetalleViaje);
        }

        // GET: Viajes/Create
        public IActionResult Create()
        {
            ViewData["Idbus"] = new SelectList(_context.TbBus, "Idbus", "Placa");
            return View();
        }

        // POST: Viajes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public TbDetalleViaje CreatePost(TbDetalleViaje pViaje)
        {
            var busBd = _context.TbBus.FirstOrDefault(x => x.Idbus == pViaje.Idbus);
            if(busBd == null)
            {
                return null;
            }

            var horasBd = _context.TbHorasViajes.FirstOrDefault(x => (x.PuntoSalida == pViaje.PuntoPartida && x.PuntoLlegada == pViaje.PuntoLlegada));
            if (horasBd == null)
            {
                horasBd = _context.TbHorasViajes.FirstOrDefault(x => (x.PuntoSalida == pViaje.PuntoLlegada && x.PuntoLlegada == pViaje.PuntoPartida));
                if (horasBd == null)
                {
                    return null;
                }
            }

            var horasViaje = horasBd.Horas;
            var horallegada = (pViaje.HoraSalida) + new TimeSpan(0, horasViaje, 0, 0);


            List<TbDetalleViaje> viajeSecondaryDb = (from TbDetalleViaje v in _context.TbDetalleViaje
                                                     where v.Idbus == pViaje.Idbus
                                                     select v).ToList();
            if(viajeSecondaryDb != null)
            {
                for (int i = 0; i < viajeSecondaryDb.Count; i++)
                {
                    var viajeI = viajeSecondaryDb[i];

                    var hSalida = viajeI.HoraSalida;
                    var hLlegada = viajeI.HoraLlegada;
                    var fSalida = viajeI.FechaSalida;

                    if (pViaje.FechaSalida == fSalida)
                    {
                        if(pViaje.HoraSalida < hSalida && horallegada < hSalida ||
                            pViaje.HoraSalida > hLlegada && horallegada > hLlegada)
                        {
                            continue;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            


            TbDetalleViaje viajeBd = new TbDetalleViaje()
            {
                Idbus = pViaje.Idbus,
                FechaSalida = pViaje.FechaSalida,
                HoraSalida = pViaje.HoraSalida,
                HoraLlegada = horallegada,
                PuntoPartida = pViaje.PuntoPartida,
                PuntoLlegada = pViaje.PuntoLlegada,
                NroAsientos = busBd.Capacidad,
                DuracionViaje = horasViaje
            };
            if (ModelState.IsValid)
            {
                _context.TbDetalleViaje.Add(viajeBd);
                _context.SaveChanges();

                pViaje.Idviaje = viajeBd.Idviaje;

                return viajeBd;
            }

            return null;
        }

        // GET: Viajes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDetalleViaje = await _context.TbDetalleViaje.FindAsync(id);
            if (tbDetalleViaje == null)
            {
                return NotFound();
            }
            ViewData["Idbus"] = new SelectList(_context.TbBus, "Idbus", "Placa", tbDetalleViaje.Idbus);
            return View(tbDetalleViaje);
        }

        // POST: Viajes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idviaje,Idbus,FechaSalida,HoraSalida,HoraLlegada,PuntoPartida,PuntoLlegada,NroAsientos,DuracionViaje")] TbDetalleViaje pViaje)
        {
            if (id != pViaje.Idviaje)
            {
                return NotFound();
            }

            TbDetalleViaje viajeBd = _context.TbDetalleViaje.FirstOrDefault(x => x.Idviaje == pViaje.Idviaje);

            if (viajeBd == null)
            {
                return BadRequest();
            }

            var horaSalida = pViaje.HoraSalida;
            var horaLlegada = pViaje.HoraLlegada;
            var duracionViaje = Math.Round(horaLlegada.Subtract(horaSalida).TotalHours);

            viajeBd.Idbus = pViaje.Idbus;
            viajeBd.FechaSalida = pViaje.FechaSalida;
            viajeBd.HoraSalida = pViaje.HoraSalida;
            viajeBd.HoraLlegada = pViaje.HoraLlegada;
            viajeBd.PuntoPartida = pViaje.PuntoPartida;
            viajeBd.PuntoLlegada = pViaje.PuntoLlegada;
            viajeBd.NroAsientos = pViaje.NroAsientos;
            viajeBd.DuracionViaje = Convert.ToInt32(duracionViaje);

            _context.Entry(viajeBd).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Viajes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDetalleViaje = await _context.TbDetalleViaje
                .Include(t => t.IdbusNavigation)
                .FirstOrDefaultAsync(m => m.Idviaje == id);
            if (tbDetalleViaje == null)
            {
                return NotFound();
            }

            return View(tbDetalleViaje);
        }

        // POST: Viajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbDetalleViaje = await _context.TbDetalleViaje.FindAsync(id);
            _context.TbDetalleViaje.Remove(tbDetalleViaje);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbDetalleViajeExists(int id)
        {
            return _context.TbDetalleViaje.Any(e => e.Idviaje == id);
        }
    }
}
