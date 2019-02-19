using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using eGoatDDD.Application.Products.Models;
using eGoatDDD.Persistence;

namespace eGoatDDD.Application.Products.Queries
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ProductsListViewModel>
    {
        private readonly eGoatDDDDbContext _context;

        public GetAllProductsQueryHandler(eGoatDDDDbContext context)
        {
            _context = context;
        }

        public async Task<ProductsListViewModel> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            // TODO: Set view model state based on user permissions.
            ProductsListViewModel model = null;

            if (request.LenderId == null || request.LenderId == string.Empty)
            {
                model = new ProductsListViewModel
                {
                    Products = await _context.Products
                        .Select(ProductDto.Projection)
                        .Where(p => p.Discontinued == false)
                        .OrderBy(p => p.ProductName).ThenBy(p => p.Created)
                        .ToListAsync(cancellationToken),
                    CreateEnabled = true
                };
            } else
            {
                model = new ProductsListViewModel
                {
                    Products = await _context.Products
                       .Select(ProductDto.Projection)
                       .Where(p => p.Discontinued == false && p.LenderId == request.LenderId)
                       .OrderBy(p => p.ProductName).ThenBy(p => p.Created)
                       .ToListAsync(cancellationToken),
                    CreateEnabled = true
                };

            }
           

            return model;
        }
    }
}
