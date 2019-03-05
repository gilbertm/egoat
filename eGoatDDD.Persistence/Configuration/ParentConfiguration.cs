using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eGoatDDD.Domain.Entities;

namespace eGoatDDD.Persistence.Configuration
{
    public class ParentConfiguration : IEntityTypeConfiguration<Parent>
    {
        public void Configure(EntityTypeBuilder<Parent> builder)
        {
            builder.HasKey(e => new { e.GoatId, e.ParentId });

            builder.HasOne(g => g.Goat)
                .WithMany(gb => gb.Parents)
                .HasForeignKey(g => g.GoatId);

            builder.Property(p => p.ParentId)
                .ValueGeneratedNever();
        }
    }
}
