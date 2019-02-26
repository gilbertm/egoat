using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eGoatDDD.Domain.Entities;

namespace eGoatDDD.Persistence.Configuration
{
    public class DisposalConfiguration : IEntityTypeConfiguration<Disposal>
    {
        public void Configure(EntityTypeBuilder<Disposal> builder)
        {
            builder.HasOne<Goat>()
                .WithOne(d => d.Disposal);
            // .HasPrincipalKey<Goat>(g => g.Id);
        }
    }
}