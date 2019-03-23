using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using eGoatDDD.Application.Parents.Models;
using eGoatDDD.Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Application.GoatResources.Models;

namespace eGoatDDD.Application.Parents.Queries
{
    public class GetParentsQueryHandler : IRequestHandler<GetParentsQuery, ParentsListViewModel>
    {
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;

        public GetParentsQueryHandler(eGoatDDDDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ParentsListViewModel> Handle(GetParentsQuery request, CancellationToken cancellationToken)
        {
            ParentsListViewModel model = null;

            var goat = await _context.Goats
                    .Where(g => g.Id == request.GoatChildId)
                    .Include(p => p.Parents)
                    .SingleOrDefaultAsync(cancellationToken);

            if (goat != null)
            {
                List<ParentViewModel> parents = null;


                if (goat.Parents.Count() > 0)
                {
                    parents = new List<ParentViewModel>();

                    foreach (var item in goat.Parents)
                    {
                        var parent = await _context.Goats
                        .Where(g => g.Id == item.ParentId)
                        .Include(c => c.Color)
                        .Include(gr => gr.GoatResources)
                        .ThenInclude(r => r.Resource)
                        .SingleOrDefaultAsync(cancellationToken);

                        if (parent != null)
                        {
                            var singleResource = parent.GoatResources.FirstOrDefault();

                            GoatResourceViewModel resource = null;

                            if (singleResource != null)
                            {
                                resource = new GoatResourceViewModel
                                {
                                    Filename = singleResource.Resource.Filename,
                                    Location = singleResource.Resource.Location,
                                    ResourceId = singleResource.Resource.ResourceId

                                };
                            }

                            parents.Add(new ParentViewModel
                            {
                                Code = parent.Code,
                                ColorName = parent.Color.Name,
                                Gender = parent.Gender,
                                ParentId = parent.Id,
                                Resource = resource
                            });
                        }

                    }
                }
                model = new ParentsListViewModel
                {
                    Goats = parents,
                    CreateEnabled = false
                };
            }

            return model;
        }
    }
}
