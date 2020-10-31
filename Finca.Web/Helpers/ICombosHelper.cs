using Finca.Web.Data.Entities;
using Finca.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Finca.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboAnimal();

        IEnumerable<SelectListItem> GetComboAnimal(TypeAnimalEntity typeAnimal);

        IEnumerable<SelectListItem> GetComboAnimal(Sex sex);

        IEnumerable<SelectListItem> GetLotes();

        IEnumerable<SelectListItem> GetTipos();
    }
}
