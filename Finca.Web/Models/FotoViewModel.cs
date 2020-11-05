using Finca.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Finca.Web.Models
{
    public class FotoViewModel : FotosAnimal
    {

        [Display(Name = "Foto")]
        public IFormFile Imagen { get; set; }
    }
}
