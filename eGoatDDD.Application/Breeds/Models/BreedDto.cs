using eGoatDDD.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace eGoatDDD.Application.Breeds.Models
{
    public class BreedDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public string Description { get; set; }

        public static Expression<Func<Breed, BreedDto>> Projection
        {
            get
            {
               
                return p => new BreedDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Picture = p.Picture,
                    Description = p.Description
                };
            }
        }

        public static BreedDto Create(Breed breed)
        {
            return Projection.Compile().Invoke(breed);
        }
    }
}