using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eGoatDDD.Application.Exceptions;
using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Persistence;
using eGoatDDD.Application.Goats.Queries;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Application.Breeds.Models;
using System.Collections.Generic;
using eGoatDDD.Application.GoatBreeds.Models;
using eGoatDDD.Application.Parents.Models;
using MediatR;
using eGoatDDD.Application.Parents.Queries;
using eGoatDDD.Application.GoatResources.Models;

namespace eGoatDDD.Application.Goats.Queries
{
    public class GetGoatQueryHandler : MediatR.IRequestHandler<GetGoatQuery, GoatNonDtoViewModel>
    {
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;

        public GetGoatQueryHandler(eGoatDDDDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<GoatNonDtoViewModel> Handle(GetGoatQuery request, CancellationToken cancellationToken)
        {
            if (request.Id > 0)
            {
                var goat = await _context.Goats
                    .Where(g => g.Id == request.Id)
                    .Include(c => c.Color)
                    .Include(gb => gb.GoatBreeds)
                        .ThenInclude(b => b.Breed)
                    .Include(p => p.Parents)
                    .Include(gr => gr.GoatResources)
                        .ThenInclude(r => r.Resource)
                    .SingleOrDefaultAsync(cancellationToken);

                Color color = new Color
                {
                    Id = goat.Color.Id,
                    Name = goat.Color.Name,
                    Description = goat.Color.Description
                };

                List<GoatBreedViewModel> breeds = null;
                if (goat.GoatBreeds.Count() > 0)
                {
                    breeds = new List<GoatBreedViewModel>();

                    foreach (var item in goat.GoatBreeds)
                    {
                        breeds.Add(new GoatBreedViewModel
                        {
                            Id = item.Breed.Id,
                            Name = item.Breed.Name,
                            Percentage = item.Percentage
                        });
                    }
                }


                ParentsListViewModel parents = await _mediator.Send(new GetParentsQuery(request.Id));

                long maternalId, sireId;
                maternalId = sireId = 0;

                foreach (var parent in goat.Parents)
                {
                    if (parent.Goat.Gender == 'M')
                    {
                        sireId = parent.ParentId;
                    }
                    if (parent.Goat.Gender == 'F')
                    {
                        maternalId = parent.ParentId;
                    }
                }

                GoatsListViewModel siblings = await _mediator.Send(new GetGoatSiblingsQuery(maternalId, sireId));


                if (goat == null)
                {
                    throw new NotFoundException(nameof(Goat), request.Id);
                }

                IList<GoatResourceViewModel> resources = (from gr in goat.GoatResources
                                                          where (gr.GoatId == goat.Id)
                                                          select new GoatResourceViewModel
                                                          {
                                                              Filename = gr.Resource.Filename,
                                                              Location = gr.Resource.Location,
                                                              ResourceId = gr.Resource.ResourceId
                                                          }).ToList();

                // TODO: Set view model state based on user permissions.
                var model = new GoatNonDtoViewModel
                {
                    Id = goat.Id,
                    BirthDate = goat.BirthDate,
                    Code = goat.Code,
                    ColorId = goat.ColorId,
                    Gender = goat.Gender,
                    Color = color,
                    Breeds = breeds,
                    Parents = parents,
                    Siblings = siblings,
                    Resources = resources,
                    EditEnabled = true,
                    DeleteEnabled = false
                };

                return model;
            }

            return null;
        }
    }
}
