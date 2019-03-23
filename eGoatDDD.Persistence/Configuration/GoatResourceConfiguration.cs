using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eGoatDDD.Domain.Entities;

namespace eGoatDDD.Persistence.Configuration
{
    public class GoatResourceConfiguration : IEntityTypeConfiguration<GoatResource>
    {
        public void Configure(EntityTypeBuilder<GoatResource> builder)
        {
            builder.HasKey(e => new { e.GoatId, e.ResourceId });

            builder.HasOne(g => g.Goat)
                .WithMany(gr => gr.GoatResources)
                .HasForeignKey(g => g.GoatId);

            builder.HasOne(r => r.Resource)
                .WithMany(gr => gr.GoatResources)
                .HasForeignKey(r => r.ResourceId);
        }
    }
}
