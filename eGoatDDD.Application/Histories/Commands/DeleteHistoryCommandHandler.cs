using eGoatDDD.Application.Services.Models;
using eGoatDDD.Application.Services.Queries;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace eGoatDDD.Application.Histories.Commands
{
    public class DeleteHistoryCommandHandler : IRequestHandler<DeleteServiceCommand, bool>
    {
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;

        public DeleteHistoryCommandHandler(
            eGoatDDDDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<bool> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                GoatService goatService = _context.GoatServices.Where(gs => gs.ServiceId == request.ServiceId).FirstOrDefault();

                if (goatService != null )
                {
                    _context.GoatServices.Remove(goatService);
                }

                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }
    }
}