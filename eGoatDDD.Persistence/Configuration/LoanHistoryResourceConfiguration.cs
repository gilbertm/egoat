using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eGoatDDD.Domain.Entities;


namespace eGoatDDD.Persistence.Configuration
{
    public class LoanHistoryResourceConfiguration : IEntityTypeConfiguration<LoanHistoryResource>
    {
        public void Configure(EntityTypeBuilder<LoanHistoryResource> builder)
        {
            builder.HasKey(e => e.LoanHistoryResourceId);

            builder.Property(e => e.LoanHistoryResourceId).HasColumnName("Id");
            builder.Property(e => e.LoanHistoryResourceId).HasColumnType("bigint");

            builder.Property(e => e.Created).HasColumnType("date");
            builder.Property(e => e.Updated).HasColumnType("date");

            builder.Property(e => e.ResourceURL).HasColumnType("ntext");

            builder.HasOne(lh => lh.LoanHistory)
                .WithMany(lhr => lhr.LoanHistoryResources)
                .HasConstraintName("FK_LoanHistory_LoanHistoryResources");

            
        }
    }
}
