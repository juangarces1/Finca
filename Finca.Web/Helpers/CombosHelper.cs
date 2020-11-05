using Finca.Web.Data;
using Finca.Web.Data.Entities;
using Finca.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Collections.Generic;
using System.Linq;

namespace Finca.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly ApplicationDbContext _context;

        public CombosHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboAnimal()
        {
            List<SelectListItem> list = _context.Animals.Select(t => new SelectListItem
            {
                Text = t.Nombre,
                Value = $"{t.Id}"
            })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un animal...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboAnimal(TypeAnimalEntity typeAnimal)
        {
            var list = _context.Animals
                .Where(a => a.TypeAnimal == typeAnimal)
                .Select(a => new SelectListItem
                {
                    Text = a.Nombre,
                    Value = $"{a.Id}"
                })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un Animal...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboAnimal(Sex sex)
        {
            var list = _context.Animals
                .Where(a => a.Sexo == sex)
                .Select(a => new SelectListItem
                {
                    Text = a.Nombre,
                    Value = $"{a.Id}"
                })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un Animal...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetLotes()
        {
            var list = _context.Lotes               
                .Select(a => new SelectListItem
                {
                    Text = a.Name,
                    Value = $"{a.Id}"
                })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un Lote...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetTipos()
        {
            var list = _context.TypeAnimals
                .Select(a => new SelectListItem
                {
                    Text = a.Name,
                    Value = $"{a.Id}"
                })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un Tipo...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetVets()
        {
            var list = _context.Veterinario
               .Select(a => new SelectListItem
               {
                   Text = a.Nombre,
                   Value = $"{a.Id}"
               })
               .OrderBy(t => t.Text)
               .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un Tipo...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetPadres()
        {
            var list = _context.Animals
               .Where(a => a.TypeAnimalId == 4)
               .Select(a => new SelectListItem
               {
                   Text = a.Nombre,
                   Value = $"{a.NumeroFinca}"
               })
               .OrderBy(t => t.Text)
               .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un Tipo...]",
                Value = "0"
            });

            return list;
        }
    }

}

