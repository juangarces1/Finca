using Finca.Web.Data.Entities;
using System.Collections.Generic;

namespace Finca.Web.Models
{
    public class ListaImagesViewModel
    {
        public int AnimalId { get; set; }

        public List<FotosAnimal> Fotos { get; set; }
    }
}
