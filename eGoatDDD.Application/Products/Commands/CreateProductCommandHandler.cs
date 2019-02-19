using eGoatDDD.Application.Products.Models;
using eGoatDDD.Application.Products.Queries;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace eGoatDDD.Application.Products.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductViewModel>
    {
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;

        public CreateProductCommandHandler(
            eGoatDDDDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ProductViewModel> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = new Product
            {
                Id = 0,
                Name = request.ProductName,
                CategoryId = request.CategoryId,
                Amount = request.Amount,
                Month = request.Month,
                Interest = request.Interest,
                IsDiminishing = request.IsDiminishing,
                Created = request.Created,
                Updated = request.Updated,
                Description = request.Description
            };

            _context.Products.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return await _mediator.Send(new GetProductQuery(entity.Id), cancellationToken);
        }
    }
}