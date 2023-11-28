using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using WSZbuss_Proyect.Models;
using WSZbuss_Proyect.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WSZbuss_Proyect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasajerosController : ControllerBase
    {
        private readonly bd_VENTAS_ZBUSSContext _context;
        public PasajerosController(bd_VENTAS_ZBUSSContext context)
        {
            _context = context;
        }
        // GET: api/<Pasajeros>
        [HttpGet]
        public ActionResult<IEnumerable<vmPasajeros>> GetPasajero()
        {
            List<vmPasajeros> list = new List<vmPasajeros>();

            var _pasajeros = (from TbPasajero b in _context.TbPasajero
                          select b).ToList();

            foreach (var item in _pasajeros)
            {
                list.Add(new vmPasajeros()
                {
                    Iduser = item.Iduser,
                    TpoDocumento = item.TpoDocumento,
                    NroDocumento = item.NroDocumento,
                    Nombre = item.Nombre,
                    ApePaterno = item.ApePaterno,
                    ApeMaterno = item.ApeMaterno,
                    Correo = item.Correo,
                    Celular = item.Celular,
                    Sexo = item.Sexo,
                    FechaNac = item.FechaNac,
                    Estado = item.Estado
                });
            }
            return Ok(list);
        }

        // GET api/<Pasajeros>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<vmPasajeros>> GetPasajero(int id)
        {
            var pasajero = await _context.TbPasajero.FindAsync(id);

            if (pasajero != null)
            {
                vmPasajeros _pasajero = new vmPasajeros()
                {
                    Iduser = pasajero.Iduser,
                    TpoDocumento = pasajero.TpoDocumento,
                    NroDocumento = pasajero.NroDocumento,
                    Nombre = pasajero.Nombre,
                    ApePaterno = pasajero.ApePaterno,
                    ApeMaterno = pasajero.ApeMaterno,
                    Correo = pasajero.Correo,
                    Celular = pasajero.Celular,
                    Sexo = pasajero.Sexo,
                    FechaNac = pasajero.FechaNac,
                    Estado = pasajero.Estado
                };
                return Ok(_pasajero);
            }

            return NotFound();
        }

        // POST api/<Pasajeros>
        [HttpPost]
        public async Task<ActionResult<vmPasajeros>> PostPasajero([FromBody] vmPasajeros pPasajeros)
        {
            if (!TbPasajeroExists(pPasajeros.NroDocumento))
            {
                TbPasajero PasajerosBd = new TbPasajero() {
                    
                    TpoDocumento = pPasajeros.TpoDocumento,
                    NroDocumento = pPasajeros.NroDocumento,
                    Nombre = pPasajeros.Nombre,
                    ApePaterno = pPasajeros.ApePaterno,
                    ApeMaterno = pPasajeros.ApeMaterno,
                    Correo = pPasajeros.Correo,
                    Celular = pPasajeros.Celular,
                    Sexo = pPasajeros.Sexo,
                    FechaNac = pPasajeros.FechaNac,
                    Estado = pPasajeros.Estado};

                if (ModelState.IsValid)
                {
                    _context.TbPasajero.Add(PasajerosBd);
                    await _context.SaveChangesAsync();

                    pPasajeros.Iduser = PasajerosBd.Iduser;

                    return Ok(pPasajeros);
                }

                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        // PUT api/<Pasajeros>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPasajero([FromBody] vmPasajeros pPasajeros)
        {
            if (pPasajeros == null)
            {
                return BadRequest();
            }

            TbPasajero PasajerosBd = _context.TbPasajero.FirstOrDefault(x => x.Iduser == pPasajeros.Iduser);

            if (PasajerosBd == null)
            {
                return BadRequest();
            }
            PasajerosBd.TpoDocumento = pPasajeros.TpoDocumento;
            PasajerosBd.NroDocumento = pPasajeros.NroDocumento;
            PasajerosBd.Nombre = pPasajeros.Nombre;
            PasajerosBd.ApePaterno = pPasajeros.ApePaterno;
            PasajerosBd.ApeMaterno = pPasajeros.ApeMaterno;
            PasajerosBd.Correo = pPasajeros.Correo;
            PasajerosBd.Celular = pPasajeros.Celular;
            PasajerosBd.Sexo = pPasajeros.Sexo;
            PasajerosBd.FechaNac = pPasajeros.FechaNac;
            PasajerosBd.Estado = pPasajeros.Estado;


            _context.Entry(PasajerosBd).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(pPasajeros);
        }

        // DELETE api/<Pasajeros>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TbPasajero>> DeletePasajero(int id)
        {
            var tbPasajero = await _context.TbPasajero.FindAsync(id);
            if (tbPasajero == null)
            {
                return NotFound();
            }

            _context.TbPasajero.Remove(tbPasajero);
            await _context.SaveChangesAsync();

            return tbPasajero;
        }

        private bool TbPasajeroExists(string NroDocumento)
        {
            return _context.TbPasajero.Any(e => e.NroDocumento == NroDocumento);
        }
    }
}
