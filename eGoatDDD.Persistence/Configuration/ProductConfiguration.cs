using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eGoatDDD.Domain.Entities;


namespace eGoatDDD.Persistence.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.Id).HasColumnType("bigint");

            builder.Property(e => e.Amount).HasColumnName("Amount");
            builder.Property(e => e.Amount).HasColumnType("float");

            builder.Property(e => e.CategoryId).HasColumnName("CategoryId");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(e => e.Description).HasColumnType("ntext");
            
            builder.Property(e => e.Picture).HasColumnType("image");

            builder.HasOne(c => c.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(c => c.CategoryId)
                .HasConstraintName("FK_Products_Categories");

            
        }
    }
}
