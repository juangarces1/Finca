﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finca.Web.Data.Entities
{
    public class FotosAnimal
    {
        public int Id { get; set; }

        public int AnimalId { get; set; }

        public string FotoPath { get; set; }

        public virtual AnimalEntity Animal { get; set; }

        public string FotoFullPath => string.IsNullOrEmpty(FotoPath)
       ? "https://haciendalasvegas.azurewebsites.net///images/noimage.png"
       : $"https://haciendalasvegas.azurewebsites.net{FotoPath.Substring(1)}";
    }
}
