using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WSZbuss_Proyect.Models;
using WSZbuss_Proyect.ViewModels;

namespace WSZbuss_Proyect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusController : ControllerBase
    {
        private readonly bd_VENTAS_ZBUSSContext _context;

        public BusController(bd_VENTAS_ZBUSSContext context)
        {
            _context = context;
        }

        // GET: api/Bus
        [HttpGet]
        public ActionResult<IEnumerable<vmBus>> GetBus()
        {
            List<vmBus> list = new List<vmBus>();

            var _buses = (from TbBus b in _context.TbBus
                          select b).ToList();

            foreach (var item in _buses)
            {
                list.Add(new vmBus()
                {
                    Idbus = item.Idbus,
                    Capacidad = item.Capacidad,
                    Pisos = item.Pisos,
                    Placa = item.Placa
                });
            }
            return Ok(list);
        }

        // GET: api/Bus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<vmBus>> GetBus(int id)
        {
            var bus = await _context.TbBus.FindAsync(id);

            if (bus != null)
            {
                vmBus _bus = new vmBus()
                {
                    Idbus = bus.Idbus,
                    Capacidad = bus.Capacidad,
                    Pisos = bus.Pisos,
                    Placa = bus.Placa
                };
                return Ok(_bus);
            }

            return NotFound();
        }

        // PUT: api/Bus/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> PutBus([FromBody] vmBus pBus)
        {
            if (pBus == null)
            {
                return BadRequest();
            }

            TbBus busBd = _context.TbBus.FirstOrDefault(x => x.Idbus == pBus.Idbus);

            if(busBd == null)
            {
                return BadRequest();
            }
            busBd.Placa = pBus.Placa;
            busBd.Capacidad = pBus.Capacidad;
            busBd.Pisos = pBus.Pisos;
           

            _context.Entry(busBd).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(pBus);
        }

        // POST: api/Bus
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<vmBus>> PostBus([FromBody] vmBus pBus)
        {
            if (!TbBusExists(pBus.Placa))
            {
                TbBus busBd = new TbBus() { Capacidad = pBus.Capacidad, Pisos = pBus.Pisos, Placa = pBus.Placa };
                if (ModelState.IsValid)
                {
                    _context.TbBus.Add(busBd);
                    await _context.SaveChangesAsync();

                    pBus.Idbus = busBd.Idbus;

                    return Ok(pBus);
                }

                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        // DELETE: api/Bus/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TbBus>> DeleteBus(int id)
        {
            var tbBus = await _context.TbBus.FindAsync(id);
            if (tbBus == null)
            {
                return NotFound();
            }

            _context.TbBus.Remove(tbBus);
            await _context.SaveChangesAsync();

            return tbBus;
        }

        private bool TbBusExists(string placa)
        {
            return _context.TbBus.Any(e => e.Placa == placa);
        }
    }
}
