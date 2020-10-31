using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Finca.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Finca.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Finca.Web.Data.Entities.AnimalEntity> Animals { get; set; }

        public DbSet<Finca.Web.Data.Entities.TypeAnimalEntity> TypeAnimals { get; set; }

        public DbSet<Finca.Web.Data.Entities.LoteEntity> Lotes { get; set; }
    }
}
