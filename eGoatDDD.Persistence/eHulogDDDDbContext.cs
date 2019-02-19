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

        public DbSet<Category> Categories { get; set; }

        public DbSet<Package> Packages { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Loan> Loans { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Ledger> Ledgers { get; set; }

        public DbSet<LoanDetail> LoanDetails { get; set; }

        public DbSet<Applicant> Applicants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyAllConfigurations();
        }
    }
}
