using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eGoatDDD.Domain.Entities;

namespace eGoatDDD.Persistence.Configuration
{
    public class PackageConfiguration : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> builder)
        {

            builder.Property(e => e.PackageId).HasColumnName("Id");

            builder.Property(e => e.PackageName)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(e => e.Description).HasColumnType("ntext");

            builder.Property(e => e.MaxLessee).HasColumnName("MaxLessee");

            builder.Property(e => e.Total).HasColumnName("Total");
        }
    }
}
