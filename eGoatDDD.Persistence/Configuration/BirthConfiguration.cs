using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eGoatDDD.Domain.Entities;

namespace eGoatDDD.Persistence.Configuration
{
    public class BirthConfiguration : IEntityTypeConfiguration<Birth>
    {
        public void Configure(EntityTypeBuilder<Birth> builder)
        {
            builder.HasKey(e => new { e.Id });

            builder.HasOne(g => g.Goat)
                .WithMany(b => b.Births)
                .HasForeignKey(g => g.GoatId);

        }
    }
}
