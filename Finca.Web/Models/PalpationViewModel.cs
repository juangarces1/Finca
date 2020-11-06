using Finca.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Finca.Web.Models
{
    public class PalpationViewModel : Palpation
    {
        [Required(ErrorMessage = "Debe Seleccionar {0}")]
        public int VeterinarioId { get; set; }

        public int AnimalId { get; set; }

        [Display(Name = "Veterinario")]
        public IEnumerable<SelectListItem> Vets { get; set; }
    }
}
