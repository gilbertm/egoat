using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence.Extension;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace eGoatDDD.Persistence
{
    public class eGoatDDDDbContext : IdentityDbContext<ApplicationUser>
    {
        public eGoatDDDDbContext(DbContextOptions<eGoatDDDDbContext> options)
           : base(options)
        {
        }

        public DbSet<Goat> Goats { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Breed> Breeds { get; set; }

        public DbSet<Parent> Parents { get; set; }

        public DbSet<GoatBreed> GoatBreeds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyAllConfigurations();
        }
    }
}
