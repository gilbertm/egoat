using System;
using System.Linq.Expressions;
using eGoatDDD.Domain.Entities;

namespace eGoatDDD.Application.Categories.Models
{
    public class CategoryDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool Discontinued { get; set; }

        public static Expression<Func<Category, CategoryDto>> Projection
        {
            get
            {
                return p => new CategoryDto
                {
                    Id = p.Id,
                    Name = p.Name
                };
            }
        }

        public static CategoryDto Create(Category category)
        {
            return Projection.Compile().Invoke(category);
        }
    }
}