using eGoatDDD.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace eGoatDDD.Application.Colors.Models
{
    public class ColorDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public static Expression<Func<Color, ColorDto>> Projection
        {
            get
            {
               
                return p => new ColorDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description
                };
            }
        }

        public static ColorDto Create(Color Color)
        {
            return Projection.Compile().Invoke(Color);
        }
    }
}