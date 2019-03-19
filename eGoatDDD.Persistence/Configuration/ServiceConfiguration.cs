using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eGoatDDD.Domain.Entities;

namespace eGoatDDD.Persistence.Configuration
{
    public class ServiceConfiguration : IEntityTypeConfiguration<GoatService>
    {
        public void Configure(EntityTypeBuilder<GoatService> builder)
        {
            builder.HasKey(e => new { e.ServiceId });

            builder.HasOne(g => g.Goat)
                .WithMany(s => s.Services)
                .HasForeignKey(g => g.GoatId);
        }
    }
}
