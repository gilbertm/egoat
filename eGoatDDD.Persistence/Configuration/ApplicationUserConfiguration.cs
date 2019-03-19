using eGoatDDD.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eGoatDDD.Persistence.Configuration
{

    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(e => new { e.Id });

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.MiddleName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.HomeAddress).HasMaxLength(50);

            builder.Property(e => e.HomeCity).HasMaxLength(50);

            builder.Property(e => e.HomeRegion).HasMaxLength(50);

            builder.Property(e => e.HomeCountryCode).HasMaxLength(50);

            builder.Property(e => e.HomePhone).HasMaxLength(50);
            
        }
    }
}
