using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eGoatDDD.Domain.Entities;


namespace eGoatDDD.Persistence.Configuration
{
    public class GoatServiceConfiguration : IEntityTypeConfiguration<GoatService>
    {
        public void Configure(EntityTypeBuilder<GoatService> builder)
        {
            builder.Property(e => e.Id).HasColumnName("Id");

            builder.Property(e => e.Service).HasColumnType("ntext");

            builder.Property(e => e.Category).HasColumnType("ntext");

            builder.Property(e => e.Start).HasColumnType("date");
            builder.Property(e => e.End).HasColumnType("date");

            builder.HasOne(g => g.Goat)
            .WithMany(gs => gs.GoatServices)
            .HasConstraintName("FK_Goat_Services");
            }
    }
}
