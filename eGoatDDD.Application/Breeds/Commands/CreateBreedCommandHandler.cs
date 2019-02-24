using eGoatDDD.Application.Breeds.Models;
using eGoatDDD.Application.Breeds.Queries;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace eGoatDDD.Application.Breeds.Commands
{
    public class CreateBreedCommandHandler : IRequestHandler<CreateBreedCommand, BreedViewModel>
    {
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;

        public CreateBreedCommandHandler(
            eGoatDDDDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<BreedViewModel> Handle(CreateBreedCommand request, CancellationToken cancellationToken)
        {
            var entity = new Breed
            {
                Id = 0,
                Name = request.Name,
                Picture = request.Picture,
                Description = request.Description
            };

            _context.Breeds.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return await _mediator.Send(new GetBreedQuery(entity.Id), cancellationToken);
        }
    }
}