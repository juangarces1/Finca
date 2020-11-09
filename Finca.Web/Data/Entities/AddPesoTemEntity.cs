using Finca.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finca.Web.Data.Entities
{
    public class AddPesoTemEntity
    {
        public int Id { get; set; }

        [Display(Name = "Peso ")]
        [Required(ErrorMessage = "Debe Ingresar el peso")]
        public decimal Peso { get; set; }

        public AnimalEntity Animal { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Debe seleccionar  el animal")]
        public int AnimalId { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> Animales { get; set; }
    }
}
