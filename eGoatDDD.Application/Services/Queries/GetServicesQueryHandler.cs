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
    public class GetServicesQueryHandler : IRequestHandler<GetServicesQuery, ServicesListViewModel>
    {
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;

        public GetServicesQueryHandler(eGoatDDDDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ServicesListViewModel> Handle(GetServicesQuery request, CancellationToken cancellationToken)
        {
            ServicesListViewModel model = null;

            var services = await _context.Services
                    .Select(ServiceDto.Projection)
                    .Where(g => g.GoatId == request.GoatId)
                    .ToListAsync(cancellationToken);

            if (services.Count() > 0)
            {
                model = new ServicesListViewModel
                {
                    Services = services,
                    CreateEnabled = false
                };
            }

            return model;
        }
    }
}
