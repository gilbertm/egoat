using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Application.Goats.Queries;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace eGoatDDD.Application.Goats.Commands
{
    public class CreateGoatCommandHandler : IRequestHandler<CreateGoatCommand, GoatViewModel>
    {
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;

        public CreateGoatCommandHandler(
            eGoatDDDDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<GoatViewModel> Handle(CreateGoatCommand request, CancellationToken cancellationToken)
        {
            var entity = new Goat
            {
                Id = 0,
                ColorId = request.ColorId,
                BreedId = request.BreedId,
                Code = request.Code,
                Picture = request.Picture,
                BirthDate = request.BirthDate,
                SlaughterDate = request.SlaughterDate
            };

            _context.Goats.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return await _mediator.Send(new GetGoatQuery(entity.Id), cancellationToken);
        }
    }
}