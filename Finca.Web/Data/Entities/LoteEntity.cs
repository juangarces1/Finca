using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Finca.Web.Data.Entities
{
    public class LoteEntity
    {
        public int Id { get; set; }

        public ICollection<AnimalEntity> Animals { get; set; }

        [Display(Name = "Lote")]
        public String Name { get; set; }
    }
}
