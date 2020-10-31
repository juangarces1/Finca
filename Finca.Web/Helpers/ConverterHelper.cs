
using Finca.Web.Data;
using Finca.Web.Data.Entities;
using Finca.Web.Models;
using System.Collections.Generic;

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
                Arete = model.Arete,
                Chapeta = model.Chapeta,
                Chip = model.Chip,
                FechaNacimiento = model.FechaNacimiento,
                Madre = model.Madre,
                Marca = model.Marca,
                MarcaInterno = model.MarcaInterno,
                Muesca = model.Muesca,
                NumeroFinca = model.NumeroFinca,
                Observaciones = model.Observaciones,
                Padre = model.Padre,
                PesoNacimiento = model.PesoNacimiento,
                Sexo = model.Sexo,
                Tatuaje = model.Tatuaje,
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
                Arete = model.Arete,
                Chapeta = model.Chapeta,
                Chip = model.Chip,
                FechaNacimiento = model.FechaNacimiento,
                Madre = model.Madre,
                Marca = model.Marca,
                MarcaInterno = model.MarcaInterno,
                Muesca = model.Muesca,
                NumeroFinca = model.NumeroFinca,
                Observaciones = model.Observaciones,
                Padre = model.Padre,
                PesoNacimiento = model.PesoNacimiento,
                Sexo = model.Sexo,
                Tatuaje = model.Tatuaje,
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
                Arete = model.Arete,
                Chapeta = model.Chapeta,
                Chip = model.Chip,
                FechaNacimiento = model.FechaNacimiento,
                Madre = model.Madre,
                Marca = model.Marca,
                MarcaInterno = model.MarcaInterno,
                Muesca = model.Muesca,
                NumeroFinca = model.NumeroFinca,
                Observaciones = model.Observaciones,
                Padre = model.Padre,
                PesoNacimiento = model.PesoNacimiento,
                Sexo = model.Sexo,
                Tatuaje = model.Tatuaje,
                TypeAnimal = model.TypeAnimal,
                LoteId = model.LoteId,
                TypeAnimalId = model.TypeAnimalId,
                Lote = model.Lote,
                FotoPath = model.FotoPath

            };
        }



    }
}

