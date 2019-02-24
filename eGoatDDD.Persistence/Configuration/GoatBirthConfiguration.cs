using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eGoatDDD.Domain.Entities;


namespace eGoatDDD.Persistence.Configuration
{
    public class GoatBirthConfiguration : IEntityTypeConfiguration<GoatBirth>
    {
        public void Configure(EntityTypeBuilder<GoatBirth> builder)
        {
            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.Id).HasColumnType("bigint");

            builder.HasOne(g => g.Goat)
                .WithMany(gb => gb.GoatBirths)
                .HasConstraintName("FK_Goat_Births");

            
        }
    }
}
