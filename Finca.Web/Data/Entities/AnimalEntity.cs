using Finca.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Finca.Web.Data.Entities
{
    public class AnimalEntity
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime FechaNacimiento { get; set; }

        [Display(Name = "Edad")]
        public int Age { get { return DateTime.Now.Year - FechaNacimiento.Year; } }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Peso Nacimiento")]
        public decimal PesoNacimiento { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Peso Destete")]
        public decimal PesoDesteto { get; set; }

        public int Padre { get; set; }

        public int Madre { get; set; }

        public int LoteId { get; set; }

        public int TypeAnimalId { get; set; }

        [Display(Name = "Numero Finca")]
        public int NumeroFinca { get; set; }

        [Display(Name = "Marca Interno")]
        public int MarcaInterno { get; set; }

     
        public string Marca { get; set; }

        [Display(Name = "Activo")]
        public bool IsActive { get; set; }

        [Display(Name = "Preñez")]
        public bool IsPrenez { get; set; }

        public string Observaciones { get; set; }

        [Display(Name = "Foto")]
        public string FotoPath { get; set; }

        public TypeAnimalEntity TypeAnimal { get; set; }

        public ICollection<PesajeEntity> Pesos { get; set; }

        public ICollection<Palpation> Palpaciones { get; set; }

        public string FotoFullPath => string.IsNullOrEmpty(FotoPath)
        ? "https://haciendalasvegas.azurewebsites.net///images/noimage.png"
        : $"https://haciendalasvegas.azurewebsites.net{FotoPath.Substring(1)}";

        [Display(Name = "Sexo")]
        public Sex Sexo { get; set; }

       
        public LoteEntity Lote { get; set; }

    }
}
