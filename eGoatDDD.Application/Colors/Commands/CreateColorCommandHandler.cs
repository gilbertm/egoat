using eGoatDDD.Application.Colors.Models;
using eGoatDDD.Application.Colors.Queries;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace eGoatDDD.Application.Colors.Commands
{
    public class CreateColorCommandHandler : IRequestHandler<CreateColorCommand, ColorViewModel>
    {
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;

        public CreateColorCommandHandler(
            eGoatDDDDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ColorViewModel> Handle(CreateColorCommand request, CancellationToken cancellationToken)
        {
            var entity = new Color
            {
                Id = 0,
                Name = request.Name,
                Description = request.Description
            };

            _context.Colors.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return await _mediator.Send(new GetColorQuery(entity.Id), cancellationToken);
        }
    }
}