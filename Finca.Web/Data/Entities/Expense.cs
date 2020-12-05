using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Finca.Web.Data.Entities
{
    public class Expense
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe Ingresar un {0}")]
        public string Detalle { get; set; }

        [Required(ErrorMessage = "Debe Ingresar una {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime Fecha { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Debe Ingresar un {0}")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }
    }
}
