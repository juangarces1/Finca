using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finca.Web.Data.Entities
{
    public class VacaHorraEntity : AnimalEntity
    {
        public string nop { get; set; }

      

        ICollection<CriasEntity> Crias { get; set; }
    }
}
