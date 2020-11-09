using Finca.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Finca.Web.Models
{
    public class PesajesViewModel 
    {
      

        

        [Required(ErrorMessage = "Debe Ingresar La fecha")]
        public DateTime Fecha { get; set; }

        public List<AddPesoTemEntity> Pesos { get; set; }
    }
}
