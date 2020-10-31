using Finca.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Finca.Web.Models
{
    public class AnimalViewModel : AnimalEntity
    {
        public IEnumerable<SelectListItem> Padres { get; set; }

        public IEnumerable<SelectListItem> Madres { get; set; }

        public IEnumerable<SelectListItem> Lotes { get; set; }

        public IEnumerable<SelectListItem> Tipos { get; set; }

        public List<AnimalEntity> Crias { get; set; }

        [Display(Name = "Foto")]
        public IFormFile ImageFile { get; set; }
    }
}
