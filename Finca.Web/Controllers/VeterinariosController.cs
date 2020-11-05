using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Finca.Web.Data;
using Finca.Web.Data.Entities;

namespace Finca.Web.Controllers
{
    public class VeterinariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VeterinariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Veterinarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Veterinario.ToListAsync());
        }

        // GET: Veterinarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinario = await _context.Veterinario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veterinario == null)
            {
                return NotFound();
            }

            return View(veterinario);
        }

        // GET: Veterinarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Veterinarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Direccion,Telefono")] Veterinario veterinario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(veterinario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(veterinario);
        }

        // GET: Veterinarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinario = await _context.Veterinario.FindAsync(id);
            if (veterinario == null)
            {
                return NotFound();
            }
            return View(veterinario);
        }

        // POST: Veterinarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Direccion,Telefono")] Veterinario veterinario)
        {
            if (id != veterinario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veterinario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeterinarioExists(veterinario.Id))
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
            return View(veterinario);
        }

        // GET: Veterinarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinario = await _context.Veterinario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veterinario == null)
            {
                return NotFound();
            }

            return View(veterinario);
        }

        // POST: Veterinarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var veterinario = await _context.Veterinario.FindAsync(id);
            _context.Veterinario.Remove(veterinario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeterinarioExists(int id)
        {
            return _context.Veterinario.Any(e => e.Id == id);
        }
    }
}
