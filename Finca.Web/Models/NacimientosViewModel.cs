using Finca.Web.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Finca.Web.Models
{
    public class NacimientosViewModel
    {
        public AnimalEntity animal { get; set; }

        [Display(Name = " Dia Nacimiento Estimado ")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime Fecha { get; set; }


    }
}
