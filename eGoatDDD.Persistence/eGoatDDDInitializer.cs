using eGoatDDD.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

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
                Name = "Native",
                Description = @"<h3>Characteristics:</h3><p>small, stocky and low set</p><h3>Color and markings:</h3><p>red, white or black or combination of these colors</p>
<h3>Mature weight:</h3><p>20 – 25 kilograms</p><h3>Ave.milk production:</h3><p>0.4 liter / day</p><h3>Lactation Period:</h3><p>187 days</p>"
            });
            Breeds.Add(2, new Breed
            {
                Name = "Anglo-Nubian",
                Description = @"<h3>Characteristics:</h3><p>Long, wide and pendulous ears, Roman nose</p><h3>Color and markings:</h3><p>Black, gray, cream, white, shades of tan, reddish brown, facial stripes</p>
<h3>Mature weight:</h3><p>60 – 75 kilograms</p><h3>Ave.milk production:</h3><p>1.5 - 2.0 liters / day</p><h3>Lactation Period:</h3><p>250 days</p>",
            });
            Breeds.Add(3, new Breed
            {
                Name = "Boer",
                Description = @"<h3>Characteristics:</h3><p>meat type with short to medium hair and horns are prominent</p><h3>Color and markings:</h3><p>reddish brown head and neck with white body & legs</p>
<h3>Mature weight:</h3><p>70 – 90 kilograms</p><h3>Ave. milk production:</h3><p>1-1.5 liters/day</p><h3>Lactation Period:</h3><p>200 days</p>",
            });

            foreach (var breed in Breeds.Values)
            {
                if (context.Breeds.Where(b => b.Name.Equals(breed.Name)).Count() <= 0)
                    context.Breeds.Add(breed);
            }

            context.SaveChanges();
        }

        private void SeedColor(eGoatDDDDbContext context)
        {
            Colors.Add(1, new Color
            {
                Name = "Orange"
            });

            Colors.Add(2, new Color
            {
                Name = "Blue"
            });

            foreach (var color in Colors.Values)
            {
                if (context.Colors.Where(b => b.Name.Equals(color.Name)).Count() <= 0)
                    context.Colors.Add(color);
            }

            context.SaveChanges();
        }

    }
}
