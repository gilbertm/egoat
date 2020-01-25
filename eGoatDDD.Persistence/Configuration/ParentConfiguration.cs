using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eGoatDDD.Domain.Entities;

namespace eGoatDDD.Persistence.Configuration
{
    public class ParentConfiguration : IEntityTypeConfiguration<GoatParent>
    {
        public void Configure(EntityTypeBuilder<GoatParent> builder)
        {
            builder.HasKey(e => new { e.GoatId, e.ParentId });

            builder.HasOne(g => g.Goat)
                .WithMany(gb => gb.Parents)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
