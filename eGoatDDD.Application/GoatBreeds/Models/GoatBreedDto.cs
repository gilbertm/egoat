using eGoatDDD.Application.Breeds.Models;
using eGoatDDD.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace eGoatDDD.Application.GoatBreeds.Models
{
    public class GoatBreedDto
    {
        public long GoatId { get; set; }

        public int BreedId { get; set; }

        public float Percentage { get; set; }

        public BreedDto Breed { get; set; }

        public static Expression<Func<GoatBreed, GoatBreedDto>> Projection
        {
            get
            {
               
                return p => new GoatBreedDto
                {
                    GoatId = p.GoatId,
                    BreedId = p.BreedId,
                    Percentage = p.Percentage,
                    Breed = BreedDto.Create(p.Breed)
                };
            }
        }

        public static GoatBreedDto Create(GoatBreed goatBreed)
        {
            return Projection.Compile().Invoke(goatBreed);
        }
    }
}