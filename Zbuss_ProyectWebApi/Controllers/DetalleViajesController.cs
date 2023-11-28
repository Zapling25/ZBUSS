using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WSZbuss_Proyect.Models;
using System.Text.RegularExpressions;
using WSZbuss_Proyect.ViewModels;

namespace WSZbuss_Proyect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleViajesController : ControllerBase
    {
        private readonly bd_VENTAS_ZBUSSContext _context;

     
        public DetalleViajesController(bd_VENTAS_ZBUSSContext context)
        {
            _context = context;
        }

        // GET: api/DetalleViajes
        [HttpGet]
        public ActionResult<IEnumerable<vmDetalleviaje>> GetDetalleviaje()
        {
            
            List<vmDetalleviaje> list = new List<vmDetalleviaje>();

            var _DetalleViaje = (from TbDetalleViaje b in _context.TbDetalleViaje
                                 select b).ToList();
            


            foreach (var item in _DetalleViaje)
            {
                list.Add(new vmDetalleviaje()
                {
                    Idviaje = item.Idviaje,
                    Idbus = item.Idbus,
                    FechaSalida = item.FechaSalida,
                    HoraSalida = item.HoraSalida,
                    HoraLlegada = item.HoraLlegada,
                    PuntoPartida = item.PuntoPartida,
                    PuntoLlegada = item.PuntoLlegada,
                    NroAsientos = item.NroAsientos,
                    DuracionViaje = item.DuracionViaje,



    });
            }
            return Ok(list); ;
        }

        // GET: api/DetalleViajes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<vmDetalleviaje>> GetTbDetalleViaje(int id)
        {
            var DetalleViaje = await _context.TbDetalleViaje.FindAsync(id);

            if (DetalleViaje != null)
            {
                vmDetalleviaje _DetalleViaje = new vmDetalleviaje()
                {
                    Idviaje = DetalleViaje.Idviaje,
                    Idbus = DetalleViaje.Idbus,
                    FechaSalida = DetalleViaje.FechaSalida,
                    HoraSalida = DetalleViaje.HoraSalida,                    
                    HoraLlegada = DetalleViaje.HoraLlegada,
                    PuntoPartida = DetalleViaje.PuntoPartida,
                    PuntoLlegada = DetalleViaje.PuntoLlegada,
                    NroAsientos = DetalleViaje.NroAsientos,
                    DuracionViaje = DetalleViaje.DuracionViaje,

                };
                return Ok(_DetalleViaje);
            }

            return NotFound();
        }

        // PUT: api/DetalleViajes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTbDetalleViaje(int id, [FromBody] vmDetalleviaje pDetalleViaje)
        {
            if (pDetalleViaje == null)
            {
                return BadRequest();
            }

            TbDetalleViaje DetalleViajeBd = _context.TbDetalleViaje.FirstOrDefault(x => x.Idviaje == pDetalleViaje.Idviaje);

            if (DetalleViajeBd == null)
            {
                return BadRequest();
            }
            DetalleViajeBd.Idbus = pDetalleViaje.Idbus;
            DetalleViajeBd.FechaSalida = pDetalleViaje.FechaSalida;
            DetalleViajeBd.HoraSalida = pDetalleViaje.HoraSalida;
            DetalleViajeBd.HoraLlegada = pDetalleViaje.HoraLlegada;
            DetalleViajeBd.PuntoPartida = pDetalleViaje.PuntoPartida;
            DetalleViajeBd.PuntoLlegada = pDetalleViaje.PuntoLlegada;
            DetalleViajeBd.NroAsientos = pDetalleViaje.NroAsientos;
            DetalleViajeBd.DuracionViaje = pDetalleViaje.DuracionViaje;



            _context.Entry(DetalleViajeBd).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(pDetalleViaje);
        }

        // POST: api/DetalleViajes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TbDetalleViaje>> PostTbDetalleViaje([FromBody] TbDetalleViaje tbDetalleViaje)
        {
            _context.TbDetalleViaje.Add(tbDetalleViaje);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTbDetalleViaje", new { id = tbDetalleViaje.Idviaje }, tbDetalleViaje);
        }

        // DELETE: api/DetalleViajes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TbDetalleViaje>> DeleteTbDetalleViaje(int id)
        {
            var tbDetalleViaje = await _context.TbDetalleViaje.FindAsync(id);
            if (tbDetalleViaje == null)
            {
                return NotFound();
            }

            _context.TbDetalleViaje.Remove(tbDetalleViaje);
            await _context.SaveChangesAsync();

            return tbDetalleViaje;
        }

        private bool TbDetalleViajeExists(int id)
        {
            return _context.TbDetalleViaje.Any(e => e.Idviaje == id);
        }
    }
}
