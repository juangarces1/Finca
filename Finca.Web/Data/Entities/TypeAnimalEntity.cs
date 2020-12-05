using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Finca.Web.Data.Entities
{
    public class TypeAnimalEntity
    {
        public int Id { get; set; }

        [Display(Name = "Nombre ")]
        public string Name { get; set; }



    }
}
