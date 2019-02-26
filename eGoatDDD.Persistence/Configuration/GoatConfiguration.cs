using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eGoatDDD.Domain.Entities;

namespace eGoatDDD.Persistence.Configuration
{
    public class GoatConfiguration : IEntityTypeConfiguration<Goat>
    {
        public void Configure(EntityTypeBuilder<Goat> builder)
        {
            builder.Property(g => g.DisposalId).IsRequired(false);

            builder.HasIndex(p => new { p.ColorId, p.Code }).IsUnique();
        }
    }
}
