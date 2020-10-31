using Finca.Web.Data.Entities;
using Finca.Web.Data.Migrations;
using Finca.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finca.Web.Data
{
    public class SeedDb
    {
        private readonly ApplicationDbContext _context;
     

        public SeedDb(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await ChecTiposAsync();
            await CheckLotesAsync();            
            await CheckAnimalsAsync();
           


        }

        private async Task CheckLotesAsync()
        {
            if (!_context.Lotes.Any())
            {
                LoteEntity Lote = new LoteEntity { Name="Destetos" };
                _context.Lotes.Add(Lote); 
                 await _context.SaveChangesAsync();
            }
        }

        private async Task ChecTiposAsync()
        {
            if (!_context.TypeAnimals.Any())
            {
                _context.TypeAnimals.Add(new TypeAnimalEntity { Name = "Novillos Levante" });
                _context.TypeAnimals.Add(new TypeAnimalEntity { Name = "Novillas Levante" });
                await _context.SaveChangesAsync();
            }
        }



        private async Task CheckAnimalsAsync()
        {
            if (!_context.Animals.Any())
            {
                TypeAnimalEntity tipo = _context.TypeAnimals.Find(1);               
                LoteEntity lote = _context.Lotes.Find(1);
                AddAnimals("Adan", tipo, lote, Sex.Macho);
                tipo = _context.TypeAnimals.Find(2);
                AddAnimals("Eva", tipo, lote, Sex.Hembra);
                await _context.SaveChangesAsync();
            }
        }

            
            private void AddAnimals(string name, TypeAnimalEntity typeAnimalEntity, LoteEntity lote, Sex sex)
        {
            _context.Animals.Add(new AnimalEntity {
                Nombre = name,
                FotoPath = $"~/images/Animals/noimage.png",
                FechaNacimiento = DateTime.Now,
                PesoNacimiento = 50,
                Padre = 0,
                Madre = 0,
                NumeroFinca = 0,
                MarcaInterno = 0,
                Tatuaje = "no",
                Arete = "no",
                TypeAnimal = typeAnimalEntity,  
                TypeAnimalId=typeAnimalEntity.Id,
                LoteId =lote.Id,
                Chapeta ="no",
                Chip ="no",
                Lote = lote,
                Marca = "no",
                Muesca ="no",
                Observaciones = "no",
                Sexo = sex 
            });
        }

    }
}
