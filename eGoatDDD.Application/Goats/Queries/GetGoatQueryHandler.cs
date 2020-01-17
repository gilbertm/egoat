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
    public class GetGoatQueryHandler : MediatR.IRequestHandler<GetGoatQuery, GoatViewModel>
    {
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;

        public GetGoatQueryHandler(eGoatDDDDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<GoatViewModel> Handle(GetGoatQuery request, CancellationToken cancellationToken)
        {
            if (request.Id > 0)
            {
                var goatDto = await _context.Goats
                     .Where(g => g.Id == request.Id)
                .Include(c => c.Color)
                .Include(gb => gb.GoatBreeds)
                .ThenInclude(b => b.Breed)
                .Include(p => p.Parents)
                .Include(gr => gr.GoatResources)
                .ThenInclude(r => r.Resource)
                .Select(
                    goat => GoatDto.Create(goat)
                ).SingleOrDefaultAsync(cancellationToken);

                if (goatDto is { } && goatDto.Parents is { })
                {
                    foreach (var parent in goatDto.Parents)
                    {
                        parent.Parent = _context.Goats.Where(goat => goat.Id == parent.ParentId).Select(GoatDto.Projection).SingleOrDefault();
                    }
                }

                return new GoatViewModel
                {
                    Goat = goatDto,
                    DeleteEnabled = false,
                    EditEnabled = false
                };
            }

            return null;
        }
    }
}
