using Finca.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Finca.Web.Models
{
    public class PalpacionesViewModel 
    {
        [Display(Name = "# Animales")]
        public int Numero { get; set; }

        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }

        public string Veterinario { get; set; }
    }
}
