using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eGoatDDD.Domain.Entities;


namespace eGoatDDD.Persistence.Configuration
{
    public class HistoryResourceConfiguration : IEntityTypeConfiguration<HistoryResource>
    {
        public void Configure(EntityTypeBuilder<HistoryResource> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.Id).HasColumnType("bigint");

            builder.Property(e => e.Created).HasColumnType("date");
            builder.Property(e => e.Updated).HasColumnType("date");

            builder.Property(e => e.ResourceURL).HasColumnType("ntext");

            builder.HasOne(lh => lh.History)
                .WithMany(lhr => lhr.HistoryResources)
                .HasConstraintName("FK_History_HistoryResources");

            
        }
    }
}
