using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using eGoatDDD.Application.Services.Models;
using eGoatDDD.Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using eGoatDDD.Domain.Entities;

namespace eGoatDDD.Application.Services.Queries
{
    public class GetServicesByServiceQueryHandler : IRequestHandler<GetServiceByServiceQuery, ServiceViewModel>
    {
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;

        public GetServicesByServiceQueryHandler(eGoatDDDDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ServiceViewModel> Handle(GetServiceByServiceQuery request, CancellationToken cancellationToken)
        {
            ServiceViewModel model = null;

            var service = await _context.GoatServices
                    .Select(ServiceDto.Projection)
                    .Where(s => s.ServiceId == request.ServiceId)
                    .SingleOrDefaultAsync(cancellationToken);

            if (service != null)
            {
                model = new ServiceViewModel
                {
                    Service = service,
                    DeleteEnabled = false,
                    EditEnabled = false
                };
            }

            return model;
        }
    }
}
