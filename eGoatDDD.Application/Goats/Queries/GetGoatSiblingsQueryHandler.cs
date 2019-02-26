using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Persistence;
using System;

namespace eGoatDDD.Application.Goats.Queries
{
    public class GetGoatSiblingsQueryHandler : IRequestHandler<GetGoatSiblingsQuery, GoatsListViewModel>
    {
        private readonly eGoatDDDDbContext _context;

        public GetGoatSiblingsQueryHandler(eGoatDDDDbContext context)
        {
            _context = context;
        }

        public async Task<GoatsListViewModel> Handle(GetGoatSiblingsQuery request, CancellationToken cancellationToken)
        {
            GoatsListViewModel model = null;

            if (request.MaternalId > 0 && request.SireId > 0)
            {
                model = new GoatsListViewModel
                {
                    Goats = await _context.Goats
                           .Select(GoatDto.Projection)
                           .Where(g => g.DisposalId == null || g.DisposalId <= 0)
                           .Where(g => (g.Parents.Where(p => p.ParentId == request.MaternalId).Count() > 0) && (g.Parents.Where(p => p.ParentId == request.SireId).Count() > 0))
                           .OrderBy(p => p.ColorId).ThenBy(g => g.Code).ThenBy(g => g.BirthDate)
                           .ToListAsync(cancellationToken),

                    CreateEnabled = true
                };
            }
            else
            {
                model = new GoatsListViewModel
                {
                    Goats = await _context.Goats
                           .Select(GoatDto.Projection)
                           .Where(g => g.DisposalId == null || g.DisposalId <= 0)
                           .Where(g => (g.Parents.Where(p => p.ParentId == request.MaternalId).Count() > 0) || (g.Parents.Where(p => p.ParentId == request.SireId).Count() > 0))
                           .OrderBy(p => p.ColorId).ThenBy(g => g.Code).ThenBy(g => g.BirthDate)
                           .ToListAsync(cancellationToken),

                    CreateEnabled = true
                };
            }

            return model;
        }
    }
}
