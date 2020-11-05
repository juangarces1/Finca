﻿using Finca.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Finca.Web.Data.Entities
{
    public class AnimalEntity
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

      
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = false)]
        public DateTime FechaNacimiento { get; set; }

        [Display(Name = "Edad")]
        public int Age { get { return DateTime.Now.Year - FechaNacimiento.Year; } }

        [Display(Name = "Peso Nacimiento")]
        public decimal PesoNacimiento { get; set; }

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

        public string Observaciones { get; set; }

        [Display(Name = "Foto")]
        public string FotoPath { get; set; }

        public TypeAnimalEntity TypeAnimal { get; set; }

        public string FotoFullPath => string.IsNullOrEmpty(FotoPath)
        ? "https://kilosmedellin.com//images/noimage.png"
        : $"https://kilosmedellin.com{FotoPath.Substring(1)}";

        [Display(Name = "Sexo")]
        public Sex Sexo { get; set; }

       
        public LoteEntity Lote { get; set; }

    }
}