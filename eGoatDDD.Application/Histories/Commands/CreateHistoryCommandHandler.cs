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
    public class CreateHistoryCommandHandler : IRequestHandler<CreateServiceCommand, ServiceViewModel>
    {
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;

        public CreateHistoryCommandHandler(
            eGoatDDDDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ServiceViewModel> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var service = new GoatService
                {
                    ServiceId = 0,
                    GoatId = request.GoatId,
                    Type = request.Type,
                    Category = request.Category,
                    Description = request.Description,
                    Start = request.Start,
                    End = request.End
                };

                _context.GoatServices.Add(service);

                await _context.SaveChangesAsync(cancellationToken);

                return await _mediator.Send(new GetServiceByServiceQuery(service.ServiceId));

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }
    }
}