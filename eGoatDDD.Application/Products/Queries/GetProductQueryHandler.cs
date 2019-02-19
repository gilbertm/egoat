using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eGoatDDD.Application.Exceptions;
using eGoatDDD.Application.Products.Models;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;

namespace eGoatDDD.Application.Products.Queries
{
    public class GetProductQueryHandler : MediatR.IRequestHandler<GetProductQuery, ProductViewModel>
    {
        private readonly eGoatDDDDbContext _context;

        public GetProductQueryHandler(eGoatDDDDbContext context)
        {
            _context = context;
        }

        public async Task<ProductViewModel> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Select(ProductDto.Projection)
                .Where(p => p.ProductId == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (product == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            // TODO: Set view model state based on user permissions.
            var model = new ProductViewModel
            {
                Product = product,
                EditEnabled = true,
                DeleteEnabled = false
            };

            return model;
        }
    }
}
