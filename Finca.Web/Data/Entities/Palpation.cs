using System;
using System.ComponentModel.DataAnnotations;

namespace Finca.Web.Data.Entities
{
    public class Palpation
    {
        public int Id { get; set; }

        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime Fecha { get; set; }

        [Display(Name = "Meses")]
        public int Meses { get; set; }

        [Display(Name = "Animal")]
        public AnimalEntity Animal { get; set; }

        [Display(Name = "Veterinario")]
        public Veterinario Veterinario { get; set; }

        [Display(Name = "Preñez")]
        public bool Estado { get; set; }
    }
}
