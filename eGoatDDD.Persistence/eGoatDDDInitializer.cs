using eGoatDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGoatDDD.Persistence
{
    public class eGoatDDDInitializer
    {
        private readonly Dictionary<int, Color> Colors = new Dictionary<int, Color>();
        private readonly Dictionary<int, Breed> Breeds = new Dictionary<int, Breed>();

        public static void Initialize(eGoatDDDDbContext context)
        {
            var initializer = new eGoatDDDInitializer();
            initializer.SeedEverything(context);
        }

        public void SeedEverything(eGoatDDDDbContext context)
        {
            context.Database.EnsureCreated();

            SeedColor(context);
            SeedBreed(context);
        }

        private void SeedBreed(eGoatDDDDbContext context)
        {
            Breeds.Add(1, new Breed
            {                 
                Id = 0,
                Name = "Mindoro local",
                Description = "Mindoro local",
            });

            foreach (var breed in Breeds.Values)
            {
                context.Breeds.Add(breed);
            }

            context.SaveChanges();
        }

        private void SeedColor(eGoatDDDDbContext context)
        {
            Colors.Add(1, new Color
            {
               
                 Id = 0,
                 Name = "Orange"
            });

            Colors.Add(1, new Color
            {

                Id = 0,
                Name = "Blue"
            });

            foreach (var color in Colors.Values)
            {
                context.Colors.Add(color);
            }

            context.SaveChanges();
        }

    }
}
