using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finca.Web.Data;
using Finca.Web.Data.Entities;
using Finca.Web.Helpers;
using Finca.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Finca.Web.Controllers
{
    public class AnimalsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly ICombosHelper _combosHelper;

        public AnimalsController(ApplicationDbContext context,
            IImageHelper imageHelper,
            IConverterHelper converterHelper,
              ICombosHelper combosHelper)
        {
            _context = context;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
            _combosHelper = combosHelper;

        }
        public IActionResult Index()
        {
            var list = _context.Animals.ToList();
            return View(list);
        }

        public  IActionResult Create()
        {
            AnimalViewModel animal = new AnimalViewModel();
            animal.Madres = _combosHelper.GetComboAnimal(Sex.Hembra);
            animal.Padres = _combosHelper.GetComboAnimal(Sex.Macho);
            animal.Lotes = _combosHelper.GetLotes();
            animal.Tipos = _combosHelper.GetTipos();
            return View(animal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnimalViewModel model)
        {
            if (ModelState.IsValid)
            {
                string path = string.Empty;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "Animals");
                }

                AnimalEntity animal = _converterHelper.ToAnimal(model, path, true);
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AnimalEntity animal =  _context.Animals
                .Find(id);
            if (animal == null)
            {
                return NotFound();
            }

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AnimalEntity animal =  _context.Animals.Find(id);
            List<AnimalEntity> hijos =  _context.Animals.Where(a => a.Padre == id || a.Madre == id).ToList();
            if (animal == null)
            {
                return NotFound();
            }
            AnimalViewModel animale =  _converterHelper.ToAnimalViewModel(animal, hijos);
            return View(animale);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AnimalEntity animale = await _context.Animals.FindAsync(id);
            if (animale == null)
            {
                return NotFound();
            }

            AnimalViewModel animal = _converterHelper.ToAnimalViewModel(animale);
            
            animal.Madres = _combosHelper.GetComboAnimal(Sex.Hembra);
            animal.Padres = _combosHelper.GetComboAnimal(Sex.Macho);
            animal.Lotes = _combosHelper.GetLotes();
            animal.Tipos = _combosHelper.GetTipos();
            return View(animal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AnimalViewModel model)
        {
            if (ModelState.IsValid)
            {
                string path = model.FotoPath;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "Animals");
                }

                AnimalEntity animal = _converterHelper.ToAnimal(model, path, false);
                _context.Update(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}
