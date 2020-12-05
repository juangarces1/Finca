using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Finca.Web.Data.Entities;

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

        public DbSet<Finca.Web.Data.Entities.FotosAnimal> FotosAnimal { get; set; }

        public DbSet<Finca.Web.Data.Entities.Expense> Expense { get; set; }

        public DbSet<Finca.Web.Data.Entities.Veterinario> Veterinario { get; set; }

        public DbSet<Finca.Web.Data.Entities.Palpation> Palpation { get; set; }

        public DbSet<Finca.Web.Data.Entities.AddPesoTemEntity> Pesos { get; set; }

        public DbSet<Finca.Web.Data.Entities.PesajeEntity> Pesajes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AnimalEntity>()
                .HasIndex(t => t.NumeroFinca)
                .IsUnique();
        }
    }
}
