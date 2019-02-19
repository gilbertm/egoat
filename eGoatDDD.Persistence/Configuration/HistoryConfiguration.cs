using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eGoatDDD.Domain.Entities;


namespace eGoatDDD.Persistence.Configuration
{
    public class HistoryConfiguration : IEntityTypeConfiguration<History>
    {
        public void Configure(EntityTypeBuilder<History> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.Id).HasColumnType("bigint");

            builder.Property(e => e.Created).HasColumnType("date");
            builder.Property(e => e.Updated).HasColumnType("date");

            builder.Property(e => e.Note).HasColumnType("ntext");
       
        }
    }
}
