using Finca.Web.Data;
using Finca.Web.Data.Entities;
using Finca.Web.Helpers;
using Finca.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            List<AnimalEntity> list = _context.Animals.OrderBy(a => a.NumeroFinca).ToList();
            return View(list);
        }

        public IActionResult IndexPrenez()
        {
            List<AnimalEntity> list = _context.Animals
                .Where(a => a.IsPrenez == true)
                .OrderBy(a => a.NumeroFinca).ToList();
            return View(list);
        }

        public IActionResult Create(int id)
        {

            AnimalViewModel animal = new AnimalViewModel
            {
                Padres = _combosHelper.GetPadres(),
                Lotes = _combosHelper.GetLotes(),
                Tipos = _combosHelper.GetTipos(),
                FechaNacimiento = DateTime.Now.Date,
                Madre = id,
                NumeroFinca = NextNumber()
            };
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
                    FotosAnimal foto = new FotosAnimal
                    {
                        Animal = animal,
                        AnimalId = animal.Id,
                        FotoPath = path,

                    };

                    _context.FotosAnimal.Add(foto);
                }

                if (model.Madre > 0)
                {
                    AnimalEntity auxMadre = _context.Animals.Find(model.Madre);
                    if(auxMadre != null ) 
                    {
                        auxMadre.IsPrenez = false;
                        _context.Animals.Update(auxMadre);
                    }
                }

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("updating"))
                    {
                        ModelState.AddModelError(string.Empty, $"Ya  existe un animal con el numero {model.NumeroFinca}.");

                    }
                    else
                    {

                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                }
            }
            model.Padres = _combosHelper.GetPadres();
            model.Lotes = _combosHelper.GetLotes();
            model.Tipos = _combosHelper.GetTipos();
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

            AnimalEntity animal = _context.Animals
                .Include(a => a.Pesos)
                .Include(a => a.Lote)
                .FirstOrDefault(a => a.Id == id);

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
            animale.NombreLote = animal.Lote.Name;
            animale.AnimalId = animal.Id;
            if (animale.Pesos != null)
            {
                if (animale.Pesos.Count > 0)
                {
                    animale.PesoActual = animal.Pesos.OrderBy(p => p.Fecha).LastOrDefault().Peso;
                }
                else
                {
                    animale.PesoActual = 0;
                }
            }
            else
            {
                animale.PesoActual = 0;
            }
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
           if (animal.IsPrenez == false) { 
            Palpation pal = _context.Palpation
                .Include(a => a.Animal)
                .OrderBy(a => a.Fecha)
                .Where(p => p.Animal.Id == animal.Id).LastOrDefault();
            TimeSpan tm = new TimeSpan();
            if (pal != null)
            {
                if (pal.Estado == true)
                {
                    return tm;
                }
                else
                {
                    AnimalEntity aniAux1 = _context.Animals
                            .Where(a => a.Madre == animal.NumeroFinca)
                            .OrderBy(a => a.FechaNacimiento)
                            .LastOrDefault();
                    if (aniAux1 != null)
                    {
                        TimeSpan tiempo = (DateTime.Now - aniAux1.FechaNacimiento);
                        return tiempo;
                    }
                    return tm;
                }
            }
            AnimalEntity aniAux = _context.Animals
                    .Where(a => a.Madre == animal.NumeroFinca)
                    .OrderBy(a => a.FechaNacimiento)
                    .LastOrDefault();
            if (aniAux != null)
            {
                TimeSpan tiempo = (DateTime.Now - aniAux.FechaNacimiento);
                return tiempo;
            }
            return tm;
            }
            TimeSpan tm1 = new TimeSpan();
            return tm1;
        }


        public DateTime ProximoParto(AnimalEntity animal)
        {
            DateTime date = new DateTime();
            if (animal.IsPrenez == true)
            {
               
                Palpation pal = _context.Palpation
                    .Include(a => a.Animal)
                    .OrderBy(a => a.Fecha)
                    .Where(p => p.Animal.Id == animal.Id).LastOrDefault();

                if (pal == null)
                {
                    return date;
                }

                if (pal.Estado == true)
                {
                    int var = 9 - pal.Meses;
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
            return date;
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
                    FotosAnimal foto = new FotosAnimal
                    {
                        Animal = animal,
                        AnimalId = animal.Id,
                        FotoPath = path,

                    };

                    _context.FotosAnimal.Add(foto);


                }


                _context.Update(animal);


                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(Details)}/{model.Id}");
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("updating"))
                    {
                        ModelState.AddModelError(string.Empty, $"Ya  existe un animal con el numero {model.NumeroFinca}.");

                    }
                    else
                    {

                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                }

            }
            model.Padres = _combosHelper.GetPadres();
            model.Lotes = _combosHelper.GetLotes();
            model.Tipos = _combosHelper.GetTipos();
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
            List<FotosAnimal> list = _context.FotosAnimal.Where(a => a.AnimalId == id).ToList();
            ListaImagesViewModel fotos = new ListaImagesViewModel
            {
                Fotos = list,
                AnimalId = animal.Id
            };
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


            List<Palpation> list = await _context
                .Palpation
                .Include(a => a.Animal)
                .Where(a => a.Animal.Id == id)
                .OrderBy(a => a.Animal.Id)
                .Include(v => v.Veterinario)
                .ToListAsync();

            PalpationListViewModel lista = new PalpationListViewModel
            {
                palpations = list,

                AnimalId = animal.Id
            };
            return View(lista);

        }
        [HttpGet]
        public async Task<IActionResult> DetailsPalpation(string date)
        {

            DateTime id = DateTime.Parse(date);

            List<Palpation> list = await _context
                .Palpation
                .Include(a => a.Animal)
                .Where(p => p.Fecha == id)
                .OrderBy(a => a.Animal.NumeroFinca)
                .Include(v => v.Veterinario)
                .ToListAsync();

            int pre = list.Where(l => l.Estado == true).Count();
            int vacia = list.Where(l => l.Estado == false).Count();

            PalpationDetailsViewModel ppp = new PalpationDetailsViewModel
            {
                Palpations = list,
                Prenez = pre,
                Vacia = vacia
            };
            return View(ppp);
        }

        public async Task<IActionResult> Palapaciones()
        {



            List<Palpation> list = await _context
                .Palpation
                .Include(v => v.Veterinario)
                .OrderBy(p => p.Fecha)
                .ToListAsync();

            DateTime date = DateTime.Now;
            List<PalpacionesViewModel> lista = new List<PalpacionesViewModel>();

            foreach (Palpation item in list)
            {
                if (date != item.Fecha)
                {
                    PalpacionesViewModel palp = new PalpacionesViewModel
                    {
                        Date = item.Fecha,
                        Veterinario = item.Veterinario.Nombre,

                    };
                    lista.Add(palp);
                    date = item.Fecha;
                }
            }

            foreach (PalpacionesViewModel item in lista)
            {
                int registros = _context.Palpation.Where(p => p.Fecha == item.Date)
                    .Count();
                item.Numero = registros;
            }

            return View(lista);

        }

        [HttpGet]
        public IActionResult AddFoto(int? id)
        {
            AnimalEntity animal = _context.Animals.Find(id);
            FotoViewModel model = new FotoViewModel
            {
                Animal = animal,
                AnimalId = animal.Id
            };

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
                    return RedirectToAction("ListaFotos", new { id = foto.AnimalId });
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

                Palpation palp = _context.Palpation.Include(v => v.Veterinario).OrderBy(p => p.Fecha).LastOrDefault();

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
                AnimalEntity animalEntity = _context.Animals.Find(model.AnimalId);
                if (model.Estado == true)
                {
                    animalEntity.IsPrenez = true;
                }
                else
                {
                    animalEntity.IsPrenez = false;
                }
                _context.Animals.Update(animalEntity);
                Palpation palpation = await _converterHelper.ToPalpationEntity(model, true);
                _context.Add(palpation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Debe Seleccionar un Veterinario";
            AnimalEntity animal = await _context.Animals.FindAsync(model.AnimalId);
            model.Animal = animal;
            model.Vets = _combosHelper.GetVets();
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
                .Include(a => a.Animal)
                .Where(p => p.Id == id).FirstOrDefault();


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
            List<FotosAnimal> list = _context.FotosAnimal.Where(a => a.AnimalId == foto.AnimalId).ToList();
            if (list.Count > 0)
            {
                FotosAnimal aux = _context.FotosAnimal.Where(a => a.AnimalId == foto.AnimalId).LastOrDefault();
                animal.FotoPath = aux.FotoPath;
            }
            {
                animal.FotoPath = "~/images/animals/noimage.png";
            }
            _context.Update(animal);
            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(ListaFotos)}/{animal.Id}");


        }

        public async Task<IActionResult> DeletePeso(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AddPesoTemEntity peso = _context.Pesos
                .Find(id);


            if (peso == null)
            {
                return NotFound();
            }

            _context.Pesos.Remove(peso);

            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(NuevoPesos)}");
        }
        public async Task<IActionResult> DeletePesaje(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PesajeEntity peso = _context.Pesajes
                .Include(p => p.Animal)
                .FirstOrDefault(p => p.Id == id);


            if (peso == null)
            {
                return NotFound();
            }

            _context.Pesajes.Remove(peso);

            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(Details)}/{peso.Animal.Id}");
        }

        [HttpGet]
        public IActionResult Nacimientos()
        {

            List<Palpation> palpations = _context.Palpation
                .Include(a => a.Animal)
                .Where(p => p.Estado == true && p.Animal.IsPrenez == true)
                .OrderBy(a => a.Fecha)
                .ToList();

           

            List<NacimientosViewModel> listaPalpacionesUltima = palpations
                                      .GroupBy(l => l.Animal.Id)
                                      .Select(cl => new NacimientosViewModel
                                      {
                                          Fecha = cl.LastOrDefault().Fecha,
                                          animal = cl.FirstOrDefault().Animal
                                      }).ToList();

            foreach (NacimientosViewModel item in listaPalpacionesUltima)
            {
                item.Fecha = ProximoParto(item.animal);
            }
            return View(listaPalpacionesUltima);
        }

        public DateTime FechaUltimoParto(AnimalEntity animal)
        {
            DateTime date = new DateTime();
            AnimalEntity aniAux1 = _context.Animals.Where(a => a.Madre == animal.NumeroFinca).OrderBy(a => a.FechaNacimiento).LastOrDefault();
           if (aniAux1 != null) {
                 date = aniAux1.FechaNacimiento;
                return date;
            }
            else
            {
                return date;
            }
        }
        public void EliminarFotoCarpeta(string ruta)
        {
            string LogoFullPath = $"wwwroot{ruta.Substring(1)}";


            if (System.IO.File.Exists(LogoFullPath) == true)
            {
                System.IO.File.Delete(LogoFullPath);
            }



        }

        public IActionResult NuevoPesos()
        {

            PesajesViewModel Var = new PesajesViewModel
            {
                Pesos = _context.Pesos
            .Include(a => a.Animal)
            .OrderBy(a => a.Animal.NumeroFinca)
            .ToList(),
                Fecha = DateTime.Now
            };

            return View(Var);

        }

        [HttpPost]
        public IActionResult NuevoPesos(PesajesViewModel model)
        {
            if (ModelState.IsValid)
            {
                int aux = _context.Pesos.Count();
                if (aux > 0)
                {
                    List<AddPesoTemEntity> pesajes = _context.Pesos.Include(p => p.Animal).ToList();
                    foreach (AddPesoTemEntity item in pesajes)
                    {
                        PesajeEntity var = new PesajeEntity
                        {
                            Animal = item.Animal,
                            Fecha = model.Fecha,
                            Peso = item.Peso
                        };
                        _context.Add(var);


                    };



                    foreach (AddPesoTemEntity item in pesajes)
                    {
                        _context.Remove(item);

                    }


                    _context.SaveChanges();

                    return RedirectToAction($"{nameof(NuevoPesos)}");
                }
                else
                {
                    ViewBag.Error = "Debe Ingresar al menos un peso a la lista";
                    model.Pesos = _context.Pesos.ToList();
                    return View(model);
                }
            }
            return View(model);

        }

        public IActionResult AddPesos()
        {

            AddPesoTemEntity Var = new AddPesoTemEntity
            {
                Animales = _combosHelper.GetComboAnimalNumeroFinca()
            };
            return View(Var);

        }

        [HttpPost]
        public IActionResult AddPesos(AddPesoTemEntity model)
        {
            if (ModelState.IsValid == true)
            {
                if (model.AnimalId != 0)
                {
                    if (model.Peso > 0)
                    {
                        AnimalEntity animal = _context.Animals.Find(model.AnimalId);
                        model.Animal = animal;
                        _context.Add(model);
                        _context.SaveChanges();
                        return RedirectToAction($"{nameof(NuevoPesos)}");
                    }
                    else
                    {
                        ViewBag.Error = "Debe Ingresar un peso";
                        model.Animales = _combosHelper.GetComboAnimalNumeroFinca();
                        return View(model);
                    }
                }
                else
                {
                    ViewBag.Error = "Debe Seleccionar un animal de la lista";
                    model.Animales = _combosHelper.GetComboAnimalNumeroFinca();
                    return View(model);
                }
            }
            return View(model);

        }

        public IActionResult CreateSon(int id)
        {

            AnimalEntity madre = _context.Animals.Find(id);

            AnimalViewModel animal = new AnimalViewModel
            {
                Padres = _combosHelper.GetPadres(),
                Lotes = _combosHelper.GetLotes(),
                Tipos = _combosHelper.GetTipos(),
                FechaNacimiento = DateTime.Now.Date,
                Madre = madre.NumeroFinca,
                MadreId = madre.Id,
                NumeroFinca = NextNumber()
            };
            return View(animal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSon(AnimalViewModel model)
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
                    FotosAnimal foto = new FotosAnimal
                    {
                        Animal = animal,
                        AnimalId = animal.Id,
                        FotoPath = path,

                    };

                    _context.FotosAnimal.Add(foto);
                }
                 

                //actualizar estado prenez madre

                if (model.Madre > 0)
                {
                    AnimalEntity auxMadre = _context.Animals.Find(model.Madre);
                    if (auxMadre != null)
                    {
                        auxMadre.IsPrenez = false;
                        _context.Animals.Update(auxMadre);
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(Details)}/{model.MadreId}");
            }

            return View(model);
        }

        public int NextNumber()
        {
            int number = 0;
            List<AnimalEntity> result = _context.Animals.ToList();
            if (result.Count > 0)
            {
                number = (from m in result
                          select m.NumeroFinca).Max();
            }
            number++;
            return number;
        }

    }
}
