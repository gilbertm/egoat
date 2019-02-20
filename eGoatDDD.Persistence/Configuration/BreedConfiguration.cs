using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eGoatDDD.Domain.Entities;

namespace eGoatDDD.Persistence.Configuration
{
    public class BreedConfiguration : IEntityTypeConfiguration<Breed>
    {
        public void Configure(EntityTypeBuilder<Breed> builder)
        {

            builder.Property(e => e.Id).HasColumnName("Id");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(e => e.Description).HasColumnType("ntext");
        }
    }
}
