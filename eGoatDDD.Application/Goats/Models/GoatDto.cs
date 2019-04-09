using eGoatDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace eGoatDDD.Application.Goats.Models
{
    public class GoatDto
    {
        public long Id { get; set; }

        public int ColorId { get; set; }

        public long? DisposalId { get; set; }

        public string Code { get; set; }

        public char Gender { get; set; }

        public ICollection<GoatResource> GoatResources { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Description { get; set; }

        public ICollection<Parent> Parents { get; set; }

        public Color Color { get; private set; }

        public virtual ICollection<GoatBreed> GoatBreeds { get; set; }

        public static Expression<Func<Goat, GoatDto>> Projection
        {
            get
            {
               
                return p => new GoatDto
                {
                    Id = p.Id,
                    DisposalId = p.DisposalId,
                    ColorId = p.ColorId,
                    Gender = p.Gender,
                    Code = p.Code,
                    BirthDate = p.BirthDate,
                    Description = p.Description,
                    Parents = p.Parents,
                    GoatResources = p.GoatResources,
                    Color = p.Color,
                    GoatBreeds = p.GoatBreeds
                };
            }
        }

        public static GoatDto Create(Goat goat)
        {
            return Projection.Compile().Invoke(goat);
        }
    }
}