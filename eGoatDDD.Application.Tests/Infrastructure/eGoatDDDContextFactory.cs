using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace eGoatDDD.Application.Tests.Infrastructure
{
    public class eGoatDDDContextFactory
    {
        public static eGoatDDDDbContext Create()
        {
            var options = new DbContextOptionsBuilder<eGoatDDDDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new eGoatDDDDbContext(options);

            context.Database.EnsureCreated();

            /* context.Goats.AddRange(new[] {
                new Goat { Id = 1,  CategoryId = 1, Name = "Cash", Amount = 150, Description = "Test 1", Picture = null, Created = DateTime.Now, Updated = DateTime.Now, Discontinued = false},
            });

            context.Colors.AddRange(new[] {
                new Category { Id = 1, Name = "Cash", Description = "Cash Loans"},
                new Category { Id = 2, Name = "Jewelry", Description = "Jewelry"},
                new Category { Id = 3, Name = "Mobile", Description = "Mobile"},
            });

            context.AppUsers.AddRange(new[] {
                new AppUser { Id = "1", FirstName = "Gilbert", LastName = "Maloloy-on"},
            });

            context.SaveChanges(); */

            return context;
        }

        public static void Destroy(eGoatDDDDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}