using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eGoatDDD.Domain.Entities;

namespace eGoatDDD.Persistence.Configuration
{
    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {

            builder.Property(e => e.Id).HasColumnName("Id");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(e => e.Description).HasColumnType("ntext");
        }
    }
}
