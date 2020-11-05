using Finca.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finca.Web.Models
{
    public class PalpationListViewModel : Palpation
    {
        public int AnimalId { get; set; }

        public List<Palpation> palpations { get; set; }
    }
}
