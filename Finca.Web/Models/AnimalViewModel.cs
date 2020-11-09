using Finca.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Finca.Web.Models
{
    public class AnimalViewModel : AnimalEntity
    {
        [Display(Name = "Padre")]
        public IEnumerable<SelectListItem> Padres { get; set; }

        [Display(Name = "Lote")]
        public IEnumerable<SelectListItem> Lotes { get; set; }

        [Display (Name ="Tipo Animal")]
        public IEnumerable<SelectListItem> Tipos { get; set; }

        public List<AnimalEntity> Crias { get; set; }

        public List<AnimalEntity> Arbol { get; set; }

        [Display(Name = "Foto")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "Lote")]
        public string NombreLote { get; set; }

        [Display(Name = "Dias Abiertos")]

        public TimeSpan Tiempo { get; set; }

        public List<FotosAnimal> Pictures { get; set; }

        public int AnimalId { get; set; }

        [Display(Name = "Proximo Parto")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = false)]
        public DateTime Prenez { get; set; }

     
        [Display(Name = "Ultimo Peso")]
        public decimal PesoActual { get; set; }
    }
}
