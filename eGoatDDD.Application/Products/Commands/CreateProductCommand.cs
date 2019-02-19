using eGoatDDD.Application.Products.Models;
using MediatR;
using System;

namespace eGoatDDD.Application.Products.Commands
{
    public class CreateProductCommand : IRequest<ProductViewModel>
    {
        public long ProductId { get; set; }

        public string ProductName { get; set; }

        public float Amount { get; set; }

        public int Month { get; set; }

        public float Interest { get; set; }

        public bool IsDiminishing { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }
        
        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public bool Discontinued { get; set; }

        public string Save { get; set; }
    }
}