using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finca.Web.Data.Entities
{
    public class Veterinario
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public ICollection<Palpation> Palpations { get; set; }

    }
}
