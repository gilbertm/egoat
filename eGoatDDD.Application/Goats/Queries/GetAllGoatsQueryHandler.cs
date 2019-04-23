using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Persistence;
using System.Collections.Generic;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Application.GoatBreeds.Models;
using eGoatDDD.Application.Parents.Models;
using eGoatDDD.Application.Parents.Queries;
using eGoatDDD.Application.GoatResources.Models;
using X.PagedList;

namespace eGoatDDD.Application.Goats.Queries
{
    public class GetAllGoatsQueryHandler : IRequestHandler<GetAllGoatsQuery, GoatsListNonDtoViewModel>
    {
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;

        public GetAllGoatsQueryHandler(eGoatDDDDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<GoatsListNonDtoViewModel> Handle(GetAllGoatsQuery request, CancellationToken cancellationToken)
        {

            IList<Goat> goats = await _context.Goats
                .Include(c => c.Color)
                .Include(gb => gb.GoatBreeds)
                .ThenInclude(b => b.Breed)
                .Include(p => p.Parents)
                .Include(gr => gr.GoatResources)
                .ThenInclude(r => r.Resource)
                .Where(g => g.DisposalId == null || g.DisposalId < 0)
                .ToListAsync(cancellationToken);

            IList<GoatNonDtoViewModel> goatFullInfo = new List<GoatNonDtoViewModel>();
            foreach (var item in goats)
            {
                Color color = new Color
                {
                    Id = item.Color.Id,
                    Name = item.Color.Name,
                    Description = item.Color.Description
                };

                List<GoatBreedViewModel> breeds = null;
                if (item.GoatBreeds.Count() > 0)
                {
                    breeds = new List<GoatBreedViewModel>();

                    foreach (var itemBreeds in item.GoatBreeds)
                    {
                        breeds.Add(new GoatBreedViewModel
                        {
                            Id = itemBreeds.Breed.Id,
                            Name = itemBreeds.Breed.Name,
                            Percentage = itemBreeds.Percentage
                        });
                    }
                }

                ParentsListViewModel parents = await _mediator.Send(new GetParentsQuery(item.Id));


                long maternalId, sireId;
                maternalId = sireId = 0;

                foreach (var parent in item.Parents)
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

                IList<GoatResourceViewModel> resources = (from gr in item.GoatResources
                                                          where (gr.GoatId == item.Id)
                                                          select new GoatResourceViewModel
                                                          {
                                                              Filename = gr.Resource.Filename,
                                                              Location = gr.Resource.Location,
                                                              ResourceId = gr.Resource.ResourceId
                                                          }).ToList();

                GoatsListViewModel siblings = await _mediator.Send(new GetGoatSiblingsQuery(maternalId, sireId));

                goatFullInfo.Add(new GoatNonDtoViewModel
                {
                    Id = item.Id,
                    ColorId = item.ColorId,
                    Code = item.Code,
                    BirthDate = item.BirthDate,
                    Gender = item.Gender,
                    Description = item.Description,
                    Color = color,
                    Breeds = breeds,
                    Resources = resources,
                    Parents = parents,
                    Siblings = siblings,

                    EditEnabled = true,
                    DeleteEnabled = true,
                });
            }

            IPagedList<GoatNonDtoViewModel> goatFullInfos = goatFullInfo.ToPagedList(request.PageNumber, request.PageSize);

            return new GoatsListNonDtoViewModel
            {
                Goats = goatFullInfos,
                TotalPages = goatFullInfo.Count(),
                CreateEnabled = false
            };
        }
    }
}
