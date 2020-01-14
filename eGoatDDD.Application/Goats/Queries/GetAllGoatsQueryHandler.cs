﻿using System.Linq;
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

            var goatsDto = await _context.Goats
                .Include(c => c.Color)
                .Include(gb => gb.GoatBreeds)
                .ThenInclude(b => b.Breed)
                .Include(p => p.Parents)
                .Include(gr => gr.GoatResources)
                .ThenInclude(r => r.Resource)
                .Select(
                    goat => GoatDto.Create(goat)
                ).ToListAsync(cancellationToken);

            request.Filter = "All";

            if (request.Filter == "Alive")
            {
                goatsDto = await goatsDto.Where(g => g.DisposalId == null || g.DisposalId < 0).ToListAsync(cancellationToken);

            } else if (request.Filter == "Deleted")
            {
                goatsDto = await goatsDto.Where(g => g.DisposalId is { } || g.DisposalId > 0).ToListAsync(cancellationToken);
            }

            ICollection<GoatViewModel> goatsViewModel = new List<GoatViewModel>(); 

            foreach (var goat in goatsDto)
            {
            
                long maternalId, sireId, goatId;
                
                maternalId = sireId = goatId = 0;

                goatId = goat.Id;

                if (goat.Parents is { })
                {
                    foreach (var parent in goat.Parents)
                    {

                        if (parent.Gender == 'M')
                        {
                            sireId = parent.GoatId;
                        }
                        if (parent.Gender == 'F')
                        {
                            maternalId = parent.GoatId;
                        }
                    }


                    goat.Siblings = null;
                    // await _mediator.Send(new GetGoatSiblingsQuery(maternalId, sireId, goatId));
                }
               

                goatsViewModel.Add(new GoatViewModel
                {
                     Goat = goat,
                     EditEnabled = true,
                     DeleteEnabled = true,
                });

            }

            return new GoatsListNonDtoViewModel
            {
                Goats = (request.PageNumber == 0 && request.PageSize == 0) ? goatsViewModel.ToPagedList() : goatsViewModel.ToPagedList(request.PageNumber, request.PageSize),
                TotalPages = goatsViewModel.Count(),
                CreateEnabled = false
            };
        }
    }
}
