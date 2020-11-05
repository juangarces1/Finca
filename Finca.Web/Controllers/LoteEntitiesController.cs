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
    public class LoteEntitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoteEntitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LoteEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Lotes
                .Include(l=>l.Animals)
                .OrderBy(l=>l.Name)
                .ToListAsync());
        }

        // GET: LoteEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loteEntity = await _context.Lotes
                .Include(l => l.Animals)
                .FirstOrDefaultAsync(m => m.Id == id);


            if (loteEntity == null)
            {
                return NotFound();
            }

            return View(loteEntity);
        }

        // GET: LoteEntities/Create
        public IActionResult Create()
        {
            LoteEntity lote = new LoteEntity();
            return View(lote);
        }

        // POST: LoteEntities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] LoteEntity loteEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loteEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loteEntity);
        }

        // GET: LoteEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loteEntity = await _context.Lotes.FindAsync(id);
            if (loteEntity == null)
            {
                return NotFound();
            }
            return View(loteEntity);
        }

        // POST: LoteEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] LoteEntity loteEntity)
        {
            if (id != loteEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loteEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoteEntityExists(loteEntity.Id))
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
            return View(loteEntity);
        }

        // GET: LoteEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loteEntity = await _context.Lotes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loteEntity == null)
            {
                return NotFound();
            }

            return View(loteEntity);
        }

        // POST: LoteEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loteEntity = await _context.Lotes.FindAsync(id);
            _context.Lotes.Remove(loteEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoteEntityExists(int id)
        {
            return _context.Lotes.Any(e => e.Id == id);
        }
    }
}
