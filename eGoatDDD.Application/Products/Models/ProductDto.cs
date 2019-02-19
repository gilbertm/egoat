using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using eGoatDDD.Domain.Entities;

namespace eGoatDDD.Application.Products.Models
{
    public class ProductDto
    {
        public long ProductId { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public float Amount { get; set; }

        public int Month { get; set; }

        public float Interest { get; set; }

        public bool IsDiminishing { get; set; }

        public string LenderId { get; set; }
        
        public string LenderName { get; set; }

        public int? CategoryId { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public bool Discontinued { get; set; }
        
        public static Expression<Func<Product, ProductDto>> Projection
        {
            get
            {
               
                return p => new ProductDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    Description = p.Description,
                    Amount = p.Amount,
                    Month = p.Month,
                    Interest = p.Interest,
                    IsDiminishing = p.IsDiminishing,
                    CategoryId = p.CategoryId,
                    Created = p.Created,
                    Updated = p.Updated,
                    Name = p.Category != null
                        ? p.Category.Name : string.Empty,
                    Discontinued = p.Discontinued
                };
            }
        }

        public static ProductDto Create(Product product)
        {
            return Projection.Compile().Invoke(product);
        }
    }
}