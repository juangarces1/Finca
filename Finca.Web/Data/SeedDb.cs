using Finca.Web.Data.Entities;
using Finca.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Finca.Web.Data
{
    public class SeedDb
    {
        private readonly ApplicationDbContext _context;


        public SeedDb(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await ChecTiposAsync();
            await CheckLotesAsync();
            //await CheckAnimalsAsync();       


        }

        private async Task CheckLotesAsync()
        {
            if (!_context.Lotes.Any())
            {

                _context.Lotes.Add(new LoteEntity { Name = "Vacas Horras" });
                _context.Lotes.Add(new LoteEntity { Name = "Vacas Paridas" });
                _context.Lotes.Add(new LoteEntity { Name = "Destetos" });
                _context.Lotes.Add(new LoteEntity { Name = "Levante" });
                _context.Lotes.Add(new LoteEntity { Name = "Vendidos" });
                _context.Lotes.Add(new LoteEntity { Name = "Marcos Levante" });
                _context.Lotes.Add(new LoteEntity { Name = "Otros" });
                _context.Lotes.Add(new LoteEntity { Name = "Muertos" });

                await _context.SaveChangesAsync();
            }
        }

        private async Task ChecTiposAsync()
        {
            if (!_context.TypeAnimals.Any())
            {
                _context.TypeAnimals.Add(new TypeAnimalEntity { Name = "Novilla" });
                _context.TypeAnimals.Add(new TypeAnimalEntity { Name = "Novillo" });
                _context.TypeAnimals.Add(new TypeAnimalEntity { Name = "Vaca" });
                _context.TypeAnimals.Add(new TypeAnimalEntity { Name = "Toro Reproductor" });
                _context.TypeAnimals.Add(new TypeAnimalEntity { Name = "Ternero" });
                _context.TypeAnimals.Add(new TypeAnimalEntity { Name = "Ternera" });

                await _context.SaveChangesAsync();
            }
        }



        private async Task CheckAnimalsAsync()
        {
            if (!_context.Animals.Any())
            {

                //AddAnimals(11, Sex.Hembra,3,1);
                //AddAnimals(23, Sex.Hembra, 3, 1);
                //AddAnimals(27, Sex.Hembra, 3, 1);
                //AddAnimals(39, Sex.Hembra, 3, 1);
                //AddAnimals(43, Sex.Hembra, 3, 1);
                //AddAnimals(51, Sex.Hembra, 3, 1);
                //AddAnimals(109, Sex.Hembra, 3, 1);
                //AddAnimals(123, Sex.Hembra, 3, 1);
                //AddAnimals(126, Sex.Hembra, 3, 1);
                //AddAnimals(143, Sex.Hembra, 3, 1);
                //AddAnimals(163, Sex.Hembra, 3, 1);
                //AddAnimals(182, Sex.Macho, 4, 1);
                //AddAnimals(191, Sex.Hembra, 3, 1);
                //AddAnimals(204, Sex.Hembra, 3, 1);
                //AddAnimals(264, Sex.Hembra, 3, 1);
                //AddAnimals(241, Sex.Hembra, 3, 1);
                //AddAnimals(243, Sex.Hembra, 3, 1);
                //AddAnimals(250, Sex.Hembra, 3, 1);
                //AddAnimals(260, Sex.Hembra, 3, 1);
                //AddAnimals(264, Sex.Hembra, 3, 1);
                //AddAnimals(265, Sex.Hembra, 3, 1);
                //AddAnimals(275, Sex.Hembra, 3, 1);
                //AddAnimals(288, Sex.Hembra, 3, 1);
                //AddAnimals(305, Sex.Hembra, 3, 1);
                //AddAnimals(306, Sex.Hembra, 3, 1);
                //AddAnimals(330, Sex.Hembra, 3, 1);
                //AddAnimals(357, Sex.Hembra, 3, 1);
                //AddAnimals(358, Sex.Hembra, 3, 1);
                //AddAnimals(360, Sex.Hembra, 3, 1);
                //AddAnimals(363, Sex.Hembra, 3, 1);
                //AddAnimals(364, Sex.Hembra, 3, 1);
                //AddAnimals(366, Sex.Hembra, 3, 1);
                //AddAnimals(367, Sex.Hembra, 3, 1);
                //AddAnimals(370, Sex.Hembra, 3, 1);
                //AddAnimals(371, Sex.Hembra, 3, 1);
                //AddAnimals(372, Sex.Hembra, 3, 1);
                //AddAnimals(386, Sex.Hembra, 3, 1);
                //AddAnimals(387, Sex.Hembra, 3, 1);
                //AddAnimals(388, Sex.Hembra, 3, 1);
                //AddAnimals(389, Sex.Hembra, 3, 1);
                //AddAnimals(390, Sex.Hembra, 3, 1);
                //AddAnimals(391, Sex.Hembra, 3, 1);
                //AddAnimals(393, Sex.Hembra, 3, 1);
                //AddAnimals(395, Sex.Hembra, 3, 1);
                //AddAnimals(398, Sex.Hembra, 3, 1);
                //AddAnimals(399, Sex.Hembra, 3, 1);
                //AddAnimals(402, Sex.Hembra, 3, 1);
                //AddAnimals(407, Sex.Hembra, 3, 1);

                //AddAnimals(410, Sex.Hembra, 3, 1);
                //AddAnimals(412, Sex.Hembra, 3, 1);
                //AddAnimals(416, Sex.Hembra, 3, 1);
                //AddAnimals(419, Sex.Hembra, 3, 1);
                //AddAnimals(423, Sex.Hembra, 3, 1);
                //AddAnimals(425, Sex.Hembra, 3, 1);
                //AddAnimals(427, Sex.Hembra, 3, 1);
                //AddAnimals(428, Sex.Hembra, 3, 1);
                //AddAnimals(429, Sex.Hembra, 3, 1);
                //AddAnimals(431, Sex.Hembra, 3, 1);
                //AddAnimals(433, Sex.Hembra, 3, 1);
                //AddAnimals(434, Sex.Hembra, 3, 1);
                //AddAnimals(436, Sex.Hembra, 3, 1);
                //AddAnimals(438, Sex.Hembra, 3, 1);
                //AddAnimals(440, Sex.Hembra, 3, 1);
                //AddAnimals(441, Sex.Hembra, 3, 1);

                //AddAnimals(442, Sex.Hembra, 3, 1);
                //AddAnimals(443, Sex.Hembra, 3, 1);
                //AddAnimals(444, Sex.Hembra, 3, 1);
                //AddAnimals(445, Sex.Hembra, 3, 1);
                //AddAnimals(446, Sex.Hembra, 3, 1);
                //AddAnimals(447, Sex.Hembra, 3, 1);
                //AddAnimals(448, Sex.Hembra, 3, 1);
                //AddAnimals(449, Sex.Hembra, 3, 1);
                //AddAnimals(450, Sex.Hembra, 3, 1);
                //AddAnimals(451, Sex.Hembra, 3, 1);
                //AddAnimals(452, Sex.Hembra, 3, 1);
                //AddAnimals(455, Sex.Hembra, 3, 1);
                //AddAnimals(456, Sex.Hembra, 3, 1);
                //AddAnimals(457, Sex.Hembra, 3, 1);
                //AddAnimals(460, Sex.Hembra, 3, 1);
                //AddAnimals(464, Sex.Hembra, 3, 1);

                //AddAnimals(469, Sex.Hembra, 3, 1);
                //AddAnimals(471, Sex.Hembra, 3, 1);
                //AddAnimals(472, Sex.Hembra, 3, 1);
                //AddAnimals(474, Sex.Hembra, 3, 1);
                //AddAnimals(481, Sex.Hembra, 3, 1);
                //AddAnimals(486, Sex.Hembra, 3, 1);





                await _context.SaveChangesAsync();
            }
        }


        private void AddAnimals(int numero, Sex sex, int tipo, int lote, string obser, string nombre, DateTime fecha,
            decimal peson, decimal pesod, bool active, int padre, int madre, string marca)
        {
            _context.Animals.Add(new AnimalEntity
            {
                Nombre = nombre,
                FotoPath = $"~/images/Animals/noimage.png",
                FechaNacimiento = fecha,
                PesoNacimiento = peson,
                Padre = padre,
                Madre = madre,
                NumeroFinca = numero,
                PesoDesteto = pesod,
                MarcaInterno = 0,
                TypeAnimalId = tipo,
                LoteId = lote,
                Marca = marca,
                Observaciones = obser,
                Sexo = sex,
                IsActive = true
            });
        }

    }
}
