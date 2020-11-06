using Finca.Web.Data;
using Finca.Web.Data.Entities;
using Finca.Web.Helpers;
using Finca.Web.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Finca.Web.Controllers
{
    public class AnimalsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IHostingEnvironment _hostingEnvironment;


        public AnimalsController(ApplicationDbContext context,
            IImageHelper imageHelper,
            IConverterHelper converterHelper,
              ICombosHelper combosHelper,
              IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
            _combosHelper = combosHelper;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            var list = _context.Animals.OrderBy(a => a.NumeroFinca).ToList();
            return View(list);
        }

        public IActionResult Create()
        {
          
            AnimalViewModel animal = new AnimalViewModel();
            animal.Padres = _combosHelper.GetPadres();
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
                bool flagFoto = true;
                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "Animals");
                    flagFoto = false;
                }
                else
                {
                    switch (model.TypeAnimalId)
                    {
                        case 1:
                            path = "~/images/Animals/Novillo.jpg";
                            break;
                        case 2:
                            path = "~/images/Animals/Novillo.jpg";
                            break;
                        case 3:
                            path = "~/images/Animals/Vaca.jpg";
                            break;
                        case 4:
                            path = "~/images/Animals/noimage.jpg";
                            break;
                      
                        case 5:
                            path = "~/images/Animals/ternero.jpg";
                            break;
                        case 6:
                            path = "~/images/Animals/ternero.jpg";
                            break;
                    }
                   
                }


                AnimalEntity animal = _converterHelper.ToAnimal(model, path, true);
                _context.Add(animal);
                if (flagFoto == false)
                {
                    var foto = new FotosAnimal
                    {
                        Animal = animal,
                        AnimalId = animal.Id,
                        FotoPath = path,

                    };

                    _context.FotosAnimal.Add(foto);
                }

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

            AnimalEntity animal = _context.Animals
                .Find(id);
            if (animal == null)
            {
                return NotFound();
            }
            _context.Animals.Remove(animal);
            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("updating"))
                {
                    ModelState.AddModelError(string.Empty, $"No se puede Borrar el animal # {animal.NumeroFinca} tiene Relaciones con otras entidades.");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                return RedirectToAction(nameof(Index));

            }

           
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AnimalEntity animal = _context.Animals.Find(id);


            LoteEntity lotenombre = _context.Lotes.Find(animal.LoteId);
            List<AnimalEntity> hijos = _context.Animals.Where(a => a.Padre == animal.NumeroFinca || a.Madre == animal.NumeroFinca).OrderBy(a => a.FechaNacimiento).ToList();
            if (animal == null)
            {
                return NotFound();
            }
            AnimalViewModel animale = _converterHelper.ToAnimalViewModel(animal, hijos);
            if (animal.Sexo == Sex.Hembra)
            {
                animale.Tiempo = DiasDesdeUltimoParto(animal);
                animale.Prenez = ProximoParto(animal);
            }
            animale.Pictures = _context.FotosAnimal.Where(f => f.AnimalId == id).ToList();
            animale.Arbol = ArbolG(animal);
            animale.NombreLote = lotenombre.Name;
            animale.AnimalId = animal.Id;
          
            return View(animale);
        }



        public List<AnimalEntity> ArbolG(AnimalEntity animal)
        {
            List<AnimalEntity> tree = new List<AnimalEntity>();


            if (animal.Madre != 0)
            {
                int x = animal.Madre;
                while (x != 0)
                {
                    AnimalEntity aniAux = _context.Animals.Where(a => a.NumeroFinca == x).FirstOrDefault();
                    if (aniAux != null)
                    {
                        tree.Add(aniAux);
                        x = aniAux.Madre;
                    }
                    else
                    {
                        x = 0;
                    }
                }
            }
            return tree;
        }

        public TimeSpan DiasDesdeUltimoParto(AnimalEntity animal)
        {           
            Palpation pal = _context.Palpation
                .Include(a => a.Animal)
                .OrderBy(a => a.Fecha)
                .Where(p => p.Animal.Id == animal.Id).LastOrDefault();
            TimeSpan tm = new TimeSpan();
            if (pal != null)
            {               
               if(pal.Estado == true)
                {
                    return tm;
                }
                else
                {
                    AnimalEntity aniAux1 = _context.Animals.Where(a => a.Madre == animal.NumeroFinca).OrderBy(a => a.FechaNacimiento).LastOrDefault();
                    if (aniAux1 != null)
                    {
                        TimeSpan tiempo = (DateTime.Now - aniAux1.FechaNacimiento);
                        return tiempo;
                    }
                    return tm;
                }               
            }
            AnimalEntity aniAux = _context.Animals.Where(a => a.Madre == animal.NumeroFinca).OrderBy(a => a.FechaNacimiento).LastOrDefault();
            if (aniAux != null)
            {
                TimeSpan tiempo = (DateTime.Now - aniAux.FechaNacimiento);
                return tiempo;
            }
            return tm;
        }


        public DateTime ProximoParto(AnimalEntity animal)
        {
            DateTime date = new DateTime();
            Palpation pal = _context.Palpation
                .Include(a=>a.Animal)
                .OrderBy(a=>a.Fecha)
                .Where(p => p.Animal.Id == animal.Id).LastOrDefault();

            if (pal==null)
            {
                return date;
            }

            if (pal.Estado == true)
            {
                int var = 9 - pal.Meses ;
                    var = var * 30;
                date = DateTime.Now;
                date = date.AddDays(var);
                return date;

            }
            else
            {
                return date;
            }

           
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
            animal.Padres = _combosHelper.GetPadres();
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
                bool flagFoto = true;
                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "Animals");
                  
                    flagFoto = false;                    

                }

                AnimalEntity animal = _converterHelper.ToAnimal(model, path, false);
                if (flagFoto == false)
                {
                    var foto = new FotosAnimal{
                    Animal = animal,
                    AnimalId = animal.Id,
                    FotoPath = path,
                   
                    };

                    _context.FotosAnimal.Add(foto);
                        

                }

           
                _context.Update(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult ListaFotos(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AnimalEntity animal = _context.Animals
                .Find(id);
            if (animal == null)
            {
                return NotFound();
            }
            var list = _context.FotosAnimal.Where(a => a.AnimalId == id).ToList();
            ListaImagesViewModel fotos = new ListaImagesViewModel();
            fotos.Fotos = list;
            fotos.AnimalId = animal.Id;
            return View(fotos);
        }

        public async Task<IActionResult> ListaPalpacion(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }


            AnimalEntity animal = await _context.Animals.FindAsync(id);


            if (animal == null)
            {
                return NotFound();
            }

            if (animal.Sexo == Sex.Macho)
            {
                return RedirectToAction($"{nameof(Details)}/{animal.Id}");
            }


            var list = await _context
                .Palpation
                .Include(a => a.Animal)
                .Where(a=>a.Animal.Id==id)
                .OrderBy(a => a.Animal.Id)
                .Include(v=>v.Veterinario)
                .ToListAsync();

            PalpationListViewModel lista = new PalpationListViewModel();
            lista.palpations = list;
            
            lista.AnimalId = animal.Id;
            return View(lista);

        }
        [HttpGet]       
        public async Task<IActionResult> DetailsPalpation(string date)
        {

            DateTime id = DateTime.Parse(date);

            var list = await _context
                .Palpation
                .Include(a => a.Animal)
                .Where(p => p.Fecha == id)
                .OrderBy(a => a.Animal.Id)
                .Include(v => v.Veterinario)
                .ToListAsync();

            int pre = list.Where(l => l.Estado == true).Count();
            int vacia = list.Where(l => l.Estado == false).Count();

            PalpationDetailsViewModel ppp = new PalpationDetailsViewModel();
            ppp.Palpations = list;
            ppp.Prenez = pre;
            ppp.Vacia = vacia;
            return View(ppp);
        }

        public async Task<IActionResult> Palapaciones()
        {



            var list = await _context
                .Palpation
                .Include(v => v.Veterinario)
                .OrderBy(p => p.Fecha)
                .ToListAsync();

            DateTime date = DateTime.Now;
            List<PalpacionesViewModel> lista = new List<PalpacionesViewModel>();
           
            foreach (var item in list)
            {
                if (date != item.Fecha)
                {
                    var palp = new PalpacionesViewModel
                    {
                        Date = item.Fecha,
                        Veterinario = item.Veterinario.Nombre,
                        
                    };
                    lista.Add(palp);
                    date = item.Fecha;
                }
            }

            foreach (var item in lista)
            {
                var registros = _context.Palpation.Where(p => p.Fecha == item.Date)
                    .Count();
                item.Numero = registros;
            }
           
            return View(lista);

        }

        [HttpGet]
        public IActionResult AddFoto(int? id)
        {
            AnimalEntity animal = _context.Animals.Find(id);
            FotoViewModel model = new FotoViewModel();
            model.Animal = animal;
            model.AnimalId = animal.Id;

            return View("AddFoto", model);
        }

      
        public async Task<IActionResult> AddFoto(FotoViewModel model)
        {
            if (ModelState.IsValid)
            {
                string path = string.Empty;

                if (model.Imagen != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.Imagen, "Animals");
                    FotosAnimal foto = _converterHelper.ToFotoAnimal(model, path, true);
                    _context.Add(foto);
                    AnimalEntity animal = await _context.Animals.FindAsync(foto.AnimalId);
                    animal.FotoPath = path;
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { id = foto.AnimalId });
                }

                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddPalpation(int? id)
        {
            {
                if (id == null)
                {
                    return NotFound();
                }

                AnimalEntity animal = await _context.Animals.FindAsync(id);
                if (animal == null)
                {
                    return NotFound();
                }

                Palpation palp =  _context.Palpation.Include(v => v.Veterinario).OrderBy(p => p.Fecha).LastOrDefault();

                PalpationViewModel model = new PalpationViewModel
                {
                    Animal = animal,
                    AnimalId = animal.Id,                   
                    Fecha = palp.Fecha,
                    Vets = _combosHelper.GetVets()                    
                };

               

                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPalpation(PalpationViewModel model)
        {
            if (model.VeterinarioId != 0)
            {              

                Palpation palpation = await _converterHelper.ToPalpationEntity(model,  true);
                _context.Add(palpation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Debe Seleccionar un Veterinario";
            AnimalEntity animal = await _context.Animals.FindAsync(model.AnimalId);
            model.Animal = animal;
            model.Vets =  _combosHelper.GetVets();
            Palpation palp = _context.Palpation.Include(v => v.Veterinario).OrderBy(p => p.Fecha).LastOrDefault();
            model.Fecha = palp.Fecha;
            return View(model);
        }
        
        public async Task<IActionResult> DeletePalpitation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Palpation pal = _context.Palpation
                .Include(a=>a.Animal)
                .Where(p=>p.Id ==id).FirstOrDefault();
           

            if (pal == null)
            {
                return NotFound();
            }

            _context.Palpation.Remove(pal);


            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(ListaPalpacion)}/{pal.Animal.Id}");


        }
        public async Task<IActionResult> DeleteFoto(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FotosAnimal foto = _context.FotosAnimal
                .Find(id);
            EliminarFotoCarpeta(foto.FotoPath);

            if (foto == null)
            {
                return NotFound();
            }

            _context.FotosAnimal.Remove(foto);



            AnimalEntity animal = _context.Animals.Find(foto.AnimalId);
            var list = _context.FotosAnimal.Where(a => a.AnimalId == foto.AnimalId).ToList();
            if (list.Count > 0)
            {
                var aux = _context.FotosAnimal.Where(a => a.AnimalId == foto.AnimalId).LastOrDefault();               
                animal.FotoPath = aux.FotoPath; 
            }
            {              
                animal.FotoPath = "~/images/animals/noimage.png";              
            }
            _context.Update(animal);
            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(ListaFotos)}/{animal.Id}");


        }

        public void EliminarFotoCarpeta(string ruta)
        {
             string LogoFullPath = $"wwwroot{ruta.Substring(1)}";


            if (System.IO.File.Exists(LogoFullPath) ==true)
            {
                System.IO.File.Delete(LogoFullPath);
            }
          


        }



    }
}
