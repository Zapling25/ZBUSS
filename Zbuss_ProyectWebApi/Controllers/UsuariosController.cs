using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WSZbuss_Proyect.Models;

namespace WSZbuss_Proyect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly bd_VENTAS_ZBUSSContext _context;

        public UsuariosController(bd_VENTAS_ZBUSSContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TbUsuarios>>> GetTbUsuarios()
        {
            return await _context.TbUsuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TbUsuarios>> GetTbUsuarios(int id)
        {
            var tbUsuarios = await _context.TbUsuarios.FindAsync(id);

            if (tbUsuarios == null)
            {
                return NotFound();
            }

            return tbUsuarios;
        }

        [HttpPost("autenticar")]
        public IActionResult Autenticar([FromBody] TbUsuarios pUsuario)
        {
            var cat = _context.TbUsuarios.FirstOrDefault(x => x.Contrasena == pUsuario.Contrasena && x.Correo == pUsuario.Correo);

            if (cat != null)
            {
                TbUsuarios obj = new TbUsuarios()
                {
                    Correo = cat.Correo,
                    Contrasena = cat.Contrasena,
                    Apellidos = cat.Apellidos,
                    Celular = cat.Celular,
                    IdRol = cat.IdRol,
                    IdUsuario = cat.IdUsuario,
                    Nombres = cat.Nombres,
                    TipoDoc = cat.TipoDoc,
                    NroDocumento = cat.NroDocumento
                };
                return Ok(obj);
            }

            return NotFound();


        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTbUsuarios(int id, [FromBody] TbUsuarios tbUsuarios)
        {
            if (id != tbUsuarios.IdUsuario)
            {
                return BadRequest();
            }

            _context.Entry(tbUsuarios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TbUsuariosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TbUsuarios>> PostTbUsuarios([FromBody] TbUsuarios tbUsuarios)
        {
            _context.TbUsuarios.Add(tbUsuarios);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTbUsuarios", new { id = tbUsuarios.IdUsuario }, tbUsuarios);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TbUsuarios>> DeleteTbUsuarios(int id)
        {
            var tbUsuarios = await _context.TbUsuarios.FindAsync(id);
            if (tbUsuarios == null)
            {
                return NotFound();
            }

            _context.TbUsuarios.Remove(tbUsuarios);
            await _context.SaveChangesAsync();

            return tbUsuarios;
        }

        private bool TbUsuariosExists(int id)
        {
            return _context.TbUsuarios.Any(e => e.IdUsuario == id);
        }
    }
}
