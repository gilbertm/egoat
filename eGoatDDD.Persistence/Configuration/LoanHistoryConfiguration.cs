using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eGoatDDD.Domain.Entities;


namespace eGoatDDD.Persistence.Configuration
{
    public class LoanHistoryConfiguration : IEntityTypeConfiguration<LoanHistory>
    {
        public void Configure(EntityTypeBuilder<LoanHistory> builder)
        {
            builder.HasKey(e => e.LoanHistoryId);

            builder.Property(e => e.LoanHistoryId).HasColumnName("Id");
            builder.Property(e => e.LoanHistoryId).HasColumnType("bigint");

            builder.Property(e => e.Created).HasColumnType("date");
            builder.Property(e => e.Updated).HasColumnType("date");

            builder.Property(e => e.Note).HasColumnType("ntext");
            
            builder.HasOne(l => l.Loan)
                .WithMany(h => h.LoanHistories)
                .HasForeignKey(h => h.LoanId)
                .HasConstraintName("FK_Loan_LoanHistories");            
        }
    }
}
