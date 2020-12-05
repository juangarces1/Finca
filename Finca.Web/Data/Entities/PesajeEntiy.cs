using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Finca.Web.Data.Entities
{
    public class PesajeEntity
    {
        public int Id { get; set; }

        public AnimalEntity Animal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Peso Nacimiento")]
        public decimal Peso { get; set; }


      
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime Fecha { get; set; }

    }
}
