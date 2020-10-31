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
    public class TypeAnimalEntitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypeAnimalEntitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypeAnimalEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeAnimals.ToListAsync());
        }

        // GET: TypeAnimalEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeAnimalEntity = await _context.TypeAnimals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeAnimalEntity == null)
            {
                return NotFound();
            }

            return View(typeAnimalEntity);
        }

        // GET: TypeAnimalEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeAnimalEntities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TypeAnimalEntity typeAnimalEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeAnimalEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeAnimalEntity);
        }

        // GET: TypeAnimalEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeAnimalEntity = await _context.TypeAnimals.FindAsync(id);
            if (typeAnimalEntity == null)
            {
                return NotFound();
            }
            return View(typeAnimalEntity);
        }

        // POST: TypeAnimalEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TypeAnimalEntity typeAnimalEntity)
        {
            if (id != typeAnimalEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeAnimalEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeAnimalEntityExists(typeAnimalEntity.Id))
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
            return View(typeAnimalEntity);
        }

        // GET: TypeAnimalEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeAnimalEntity = await _context.TypeAnimals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeAnimalEntity == null)
            {
                return NotFound();
            }

            return View(typeAnimalEntity);
        }

        // POST: TypeAnimalEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeAnimalEntity = await _context.TypeAnimals.FindAsync(id);
            _context.TypeAnimals.Remove(typeAnimalEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeAnimalEntityExists(int id)
        {
            return _context.TypeAnimals.Any(e => e.Id == id);
        }
    }
}
