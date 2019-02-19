using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eGoatDDD.Domain.Entities;


namespace eGoatDDD.Persistence.Configuration
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.Property(e => e.Id).HasColumnName("Id");

            builder.Property(e => e.Type).HasColumnType("ntext");

            builder.Property(e => e.Category).HasColumnType("ntext");

            builder.Property(e => e.Start).HasColumnType("date");
            builder.Property(e => e.End).HasColumnType("date");
        }
    }
}
