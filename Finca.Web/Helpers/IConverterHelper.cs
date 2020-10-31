using Finca.Web.Data.Entities;
using Finca.Web.Models;
using System.Collections.Generic;

namespace Finca.Web.Helpers
{
    public interface IConverterHelper
    {

        AnimalEntity ToAnimal(AnimalViewModel model, string path, bool isNew);

        AnimalViewModel ToAnimalViewModel(AnimalEntity model, List<AnimalEntity> crias);

        AnimalViewModel ToAnimalViewModel(AnimalEntity model);


    }
}
