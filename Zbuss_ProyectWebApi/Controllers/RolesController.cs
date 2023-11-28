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
    public class RolesController : ControllerBase
    {
        private readonly bd_VENTAS_ZBUSSContext _context;

        public RolesController(bd_VENTAS_ZBUSSContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        public ActionResult<IEnumerable<vmRoles>> GetTbRoles()
        {
            List<vmRoles> list = new List<vmRoles>();

            var _roles = (from TbRoles b in _context.TbRoles
                          select b).ToList();

            foreach (var item in _roles)
            {
                list.Add(new vmRoles()
                {
                    Id = item.Id,
                    Descripcion = item.Descripcion,
                    Estado = item.Estado
                    
                });
            }
            return Ok(list);
        }
       

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<vmRoles>> GetTbRoles(int id)
        {
            var roles = await _context.TbRoles.FindAsync(id);

            if (roles != null)
            {
                vmRoles _roles = new vmRoles()
                {
                    Id = roles.Id,
                    Descripcion = roles.Descripcion,
                    Estado = roles.Estado
                };
                return Ok(_roles);
    }

            return NotFound();
}

// PUT: api/Roles/5
// To protect from overposting attacks, enable the specific properties you want to bind to, for
// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
[HttpPut("{id}")]
        public async Task<IActionResult> PutTbRoles([FromBody] vmRoles pRoles)
        {
            if (pRoles == null)
            {
                return BadRequest();
            }

            TbRoles rolesBd = _context.TbRoles.FirstOrDefault(x => x.Id == pRoles.Id);

            if (rolesBd == null)
            {
                return BadRequest();
            }
            
            rolesBd.Descripcion = pRoles.Descripcion;
            rolesBd.Estado = pRoles.Estado;

            _context.Entry(rolesBd).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(pRoles);
        }

        // POST: api/Roles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<vmRoles>> PostTbRoles([FromBody] vmRoles pRoles)
        {
            if (!TbRolesExists(pRoles.Estado))
            {
                TbRoles rolesBd = new TbRoles() { 
                    Descripcion = pRoles.Descripcion, 
                    Estado = pRoles.Estado, 
                    };
                if (ModelState.IsValid)
                {
                    _context.TbRoles.Add(rolesBd);
                    await _context.SaveChangesAsync();

                    pRoles.Id = rolesBd.Id;

                    return Ok(pRoles);
                }

                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }
        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TbRoles>> DeleteTbRoles(int id)
        {
            var tbRoles = await _context.TbRoles.FindAsync(id);
            if (tbRoles == null)
            {
                return NotFound();
            }

            _context.TbRoles.Remove(tbRoles);
            await _context.SaveChangesAsync();

            return tbRoles;
        }

        private bool TbRolesExists(bool estado)
        {
            return _context.TbRoles.Any(e => e.Estado == estado);
        }
    }
}
