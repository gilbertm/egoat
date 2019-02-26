using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eGoatDDD.Domain.Entities;

namespace eGoatDDD.Persistence.Configuration
{
    public class GoatBreedConfiguration : IEntityTypeConfiguration<GoatBreed>
    {
        public void Configure(EntityTypeBuilder<GoatBreed> builder)
        {
            builder.HasKey(e => new { e.GoatId, e.BreedId });

            builder.HasOne(g => g.Goat)
                .WithMany(gb => gb.GoatBreeds)
                .HasForeignKey(g => g.GoatId);

            builder.HasOne(b => b.Breed)
                .WithMany(gb => gb.GoatBreeds)
                .HasForeignKey(b => b.BreedId);
        }
    }
}
