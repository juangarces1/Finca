using Finca.Web.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Finca.Web.Models
{
    public class PalpationDetailsViewModel
    {
        public List<Palpation> Palpations { get; set; }

        [Display(Name = "Preñadas")]
        public int Prenez { get; set; }
       
        [Display(Name = "Vacias")]
        public int Vacia { get; set; }
    }
}
