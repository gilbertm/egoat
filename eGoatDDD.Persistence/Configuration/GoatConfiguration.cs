using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eGoatDDD.Domain.Entities;


namespace eGoatDDD.Persistence.Configuration
{
    public class GoatConfiguration : IEntityTypeConfiguration<Goat>
    {
        public void Configure(EntityTypeBuilder<Goat> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.Id).HasColumnType("bigint");
            
            builder.HasOne(c => c.Color)
                .WithMany(g => g.Goats)
                .HasForeignKey(c => c.ColorId)
                .HasConstraintName("FK_Goat_Colors");

            builder.HasOne(b => b.Breed)
                .WithMany(g => g.Goats)
                .HasForeignKey(b => b.BreedId)
                .HasConstraintName("FK_Goat_Breeds");


        }
    }
}
