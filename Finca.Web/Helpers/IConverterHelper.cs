using Finca.Web.Data.Entities;
using Finca.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finca.Web.Helpers
{
    public interface IConverterHelper
    {

        AnimalEntity ToAnimal(AnimalViewModel model, string path, bool isNew);

        AnimalViewModel ToAnimalViewModel(AnimalEntity model, List<AnimalEntity> crias);

        AnimalViewModel ToAnimalViewModel(AnimalEntity model);

        FotosAnimal ToFotoAnimal(FotoViewModel model, string path, bool isNew);

        FotoViewModel ToFotosViewModel(FotosAnimal foto);

        Task<Palpation> ToPalpationEntity(PalpationViewModel model, bool isNew);

        PalpationViewModel ToPalpationViewModel(Palpation model);
    }
}
