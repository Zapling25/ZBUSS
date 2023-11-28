using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zbuss_Proyect.Models;

namespace Zbuss_Proyect.Controllers
{
    public class RolesController : Controller
    {
        private readonly bd_VENTAS_ZBUSSContext _context;

        public RolesController(bd_VENTAS_ZBUSSContext context)
        {
            _context = context;
        }

        // GET: Roles
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbRoles.ToListAsync());
        }

        public List<TbRoles> RecuperarRoles()
        {
            var rolesBd = (from TbRoles r in _context.TbRoles where r.Estado == true select r).ToList();
            return rolesBd;
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbRoles = await _context.TbRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbRoles == null)
            {
                return NotFound();
            }

            return View(tbRoles);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Estado")] TbRoles tbRoles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbRoles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbRoles);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbRoles = await _context.TbRoles.FindAsync(id);
            if (tbRoles == null)
            {
                return NotFound();
            }
            return View(tbRoles);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Estado")] TbRoles tbRoles)
        {
            if (id != tbRoles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbRoles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbRolesExists(tbRoles.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tbRoles);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbRoles = await _context.TbRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbRoles == null)
            {
                return NotFound();
            }

            return View(tbRoles);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbRoles = await _context.TbRoles.FindAsync(id);
            _context.TbRoles.Remove(tbRoles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbRolesExists(int id)
        {
            return _context.TbRoles.Any(e => e.Id == id);
        }
    }
}
