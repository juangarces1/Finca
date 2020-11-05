
using Finca.Web.Data;
using Finca.Web.Data.Entities;
using Finca.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finca.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly ApplicationDbContext _context;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(ApplicationDbContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }


        public AnimalEntity ToAnimal(AnimalViewModel model, string path, bool isNew)
        {
            return new AnimalEntity
            {
                Id = isNew ? 0 : model.Id,
                FotoPath = path,
                Nombre = model.Nombre,              
                FechaNacimiento = model.FechaNacimiento,
                Madre = model.Madre,
                Marca = model.Marca,
                MarcaInterno = model.MarcaInterno,
                PesoDesteto=model.PesoDesteto,
                NumeroFinca = model.NumeroFinca,
                Observaciones = model.Observaciones,
                Padre = model.Padre,
                PesoNacimiento = model.PesoNacimiento,
                Sexo = model.Sexo,
                IsActive = model.IsActive,
                TypeAnimal = model.TypeAnimal,
                LoteId=model.LoteId,
                TypeAnimalId=model.TypeAnimalId,
                Lote=model.Lote
                 

            };
        }

        public AnimalViewModel ToAnimalViewModel(AnimalEntity model, List<AnimalEntity> crias)
        {
            return new AnimalViewModel
            {
               
                Nombre = model.Nombre,               
                FechaNacimiento = model.FechaNacimiento,
                Madre = model.Madre,
                Marca = model.Marca,
                MarcaInterno = model.MarcaInterno,
                IsActive = model.IsActive,
                NumeroFinca = model.NumeroFinca,
                Observaciones = model.Observaciones,
                Padre = model.Padre,
                PesoNacimiento = model.PesoNacimiento,
                PesoDesteto = model.PesoDesteto,
                Sexo = model.Sexo,               
                TypeAnimal = model.TypeAnimal,
                LoteId = model.LoteId,
                TypeAnimalId = model.TypeAnimalId,
                Lote = model.Lote,
                Crias = crias,
                FotoPath = model.FotoPath


            };
        }

        public AnimalViewModel ToAnimalViewModel(AnimalEntity model)
        {
            return new AnimalViewModel
            {
                Nombre = model.Nombre,             
                FechaNacimiento = model.FechaNacimiento,
                Madre = model.Madre,
                Marca = model.Marca,
                MarcaInterno = model.MarcaInterno,              
                NumeroFinca = model.NumeroFinca,
                Observaciones = model.Observaciones,
                Padre = model.Padre,
                PesoNacimiento = model.PesoNacimiento,
                PesoDesteto = model.PesoDesteto,
                Sexo = model.Sexo,
                IsActive = model.IsActive,               
                TypeAnimal = model.TypeAnimal,
                LoteId = model.LoteId,
                TypeAnimalId = model.TypeAnimalId,
                Lote = model.Lote,
                FotoPath = model.FotoPath

            };
        }

        public FotosAnimal ToFotoAnimal(FotoViewModel model, string path, bool isNew)
        {
            return new FotosAnimal
            {
                Id = isNew ? 0 : model.Id,
                Animal = model.Animal,
                AnimalId = model.AnimalId,
                FotoPath = path
            };
        }

        public FotoViewModel ToFotosViewModel(FotosAnimal model)
        {
            return new FotoViewModel
            {
                Animal = model.Animal,
                AnimalId = model.AnimalId,
                FotoPath = model.FotoPath,
                Id = model.Id
            };
        }

        public async Task<Palpation> ToPalpationEntity(PalpationViewModel model, bool isNew)
        {
            return new Palpation
            {
                Animal = await _context.Animals.FindAsync(model.AnimalId),
                Veterinario = await _context.Veterinario.FindAsync(model.VeterinarioId),
                Fecha = model.Fecha,
                Id = isNew ? 0 : model.Id,
                Prenez =model.Prenez,
                Estado = model.Estado
            };
        }

        public  PalpationViewModel ToPalpationViewModel(Palpation model)
        {
            return new PalpationViewModel
            {
                Animal=model.Animal,
                Veterinario = model.Veterinario,
                AnimalId=model.Animal.Id,
                VeterinarioId = model.Veterinario.Id,
                Fecha = model.Fecha,
                Id=model.Id,
                Prenez = model.Prenez,
                Estado= model.Estado
               
            };
        }


    }
}

