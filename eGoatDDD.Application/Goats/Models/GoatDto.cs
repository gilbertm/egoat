using eGoatDDD.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace eGoatDDD.Application.Goats.Models
{
    public class GoatDto
    {
        public long Id { get; set; }

        public int ColorId { get; set; }

        public int BreedId { get; set; }

        public string Code { get; set; }

        public string Picture { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime SlaughterDate { get; set; }
                
        public static Expression<Func<Goat, GoatDto>> Projection
        {
            get
            {
               
                return p => new GoatDto
                {
                    Id = p.Id,
                    ColorId = p.ColorId,
                    BreedId = p.BreedId,
                    Code = p.Code,
                    Picture = p.Picture,
                    BirthDate = p.BirthDate,
                    SlaughterDate = p.SlaughterDate
                };
            }
        }

        public static GoatDto Create(Goat goat)
        {
            return Projection.Compile().Invoke(goat);
        }
    }
}