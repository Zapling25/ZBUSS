using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zbuss_Proyect.Models;

namespace Zbuss_Proyect.Controllers
{
    public class MantenimientoUController : Controller
    {
        private readonly bd_VENTAS_ZBUSSContext _context;

        public MantenimientoUController(bd_VENTAS_ZBUSSContext context)
        {
            _context = context;
        }

        // GET: MantenimientoU
        public ActionResult Index(string searchString)
        {
            var usersDb = from m in _context.TbUsuarios
                          select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                usersDb = usersDb.Where(u => u.NroDocumento.Contains(searchString));
            }

            return View(usersDb);
        }

        // GET: MantenimientoU/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbUsuarios = await _context.TbUsuarios
                .Include(t => t.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (tbUsuarios == null)
            {
                return NotFound();
            }

            return View(tbUsuarios);
        }

        // GET: MantenimientoU/Create
        public IActionResult Create()
        {
            ViewData["Rol"] = new SelectList(_context.TbRoles, "Id", "Descripcion");
            return View();
        }

        // POST: MantenimientoU/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public TbUsuarios Create(TbUsuarios pUser)
        {
            if (!TbUsuariosExists(pUser.Correo))
            {

                TbUsuarios userBd = new TbUsuarios()
                {
                    TipoDoc = pUser.TipoDoc,
                    NroDocumento = pUser.NroDocumento,
                    Nombres = pUser.Nombres,
                    Apellidos = pUser.Apellidos,
                    Correo = pUser.Correo,
                    Contrasena = pUser.Contrasena,
                    Celular = pUser.Celular,
                    Rol = pUser.Rol,
                    Estado = pUser.Estado
                };
                if (ModelState.IsValid)
                {
                    _context.TbUsuarios.Add(userBd);
                    _context.SaveChanges();

                    pUser.IdUsuario = userBd.IdUsuario;

                    return userBd;
                }

                return null;
            }
            ViewBag.Alert = "El correo ingresado ya está asociado a una cuenta. Pruebe con otro";
            return null;
        }

        public TbUsuarios RecuperarUsuario(int? idUsuario)
        {
            if(idUsuario == null)
            {
                return null;
            }

            TbUsuarios userBd = _context.TbUsuarios.FirstOrDefault(x => x.IdUsuario == idUsuario);
            if (userBd == null)
            {
                return null;
            }

            return userBd;
        }

        // GET: MantenimientoU/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbUsuarios = await _context.TbUsuarios.FindAsync(id);
            if (tbUsuarios == null)
            {
                return NotFound();
            }
            ViewData["Rol"] = new SelectList(_context.TbRoles, "Id", "Descripcion", tbUsuarios.Rol);
            return View(tbUsuarios);
        }

        // POST: MantenimientoU/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public TbUsuarios Actualizar(TbUsuarios pUser)
        {
            //if (id != pUser.IdUsuario)
            //{
            //    return NotFound();
            //}

            if (pUser.TipoDoc == null || pUser.NroDocumento == null || pUser.Nombres == null ||
                pUser.Apellidos == null || pUser.Correo == null || pUser.Contrasena == null ||
                pUser.Celular == null)
            {
                return null;
            }

            TbUsuarios userBd = _context.TbUsuarios.FirstOrDefault(x => x.IdUsuario == pUser.IdUsuario);

            if (userBd == null)
            {
                return null;
            }

            userBd.TipoDoc = pUser.TipoDoc;
            userBd.NroDocumento = pUser.NroDocumento;
            userBd.Nombres = pUser.Nombres;
            userBd.Apellidos = pUser.Apellidos;
            userBd.Correo = pUser.Correo;
            userBd.Contrasena = pUser.Contrasena;
            userBd.Celular = pUser.Celular;
            userBd.Rol = pUser.Rol;
            userBd.Estado = pUser.Estado;

            _context.Entry(userBd).State = EntityState.Modified;
            _context.SaveChanges();
            return userBd;
        }

        // GET: MantenimientoU/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbUsuarios = await _context.TbUsuarios
                .Include(t => t.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (tbUsuarios == null)
            {
                return NotFound();
            }

            return View(tbUsuarios);
        }

        // POST: MantenimientoU/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbUsuarios = await _context.TbUsuarios.FindAsync(id);
            _context.TbUsuarios.Remove(tbUsuarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbUsuariosExists(string correo)
        {
            return _context.TbUsuarios.Any(e => e.Correo == correo);
        }
    }
}
