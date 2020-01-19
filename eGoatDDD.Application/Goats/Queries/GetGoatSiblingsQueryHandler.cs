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
            var goatsDto= await _context.Goats
                           .Include(c => c.Color)
                .Include(gb => gb.GoatBreeds)
                .ThenInclude(b => b.Breed)
                .Include(p => p.Parents)
                .Include(gr => gr.GoatResources)
                .ThenInclude(r => r.Resource)
                .Where(g => g.Id != request.GoatId)
                .Where(g => g.Parents != null && ((g.Parents.Where(p => p.ParentId == request.MaternalId).Count() > 0) || (g.Parents.Where(p => p.ParentId == request.SireId).Count() > 0)))
                .Select(GoatDto.Projection)
                .Where(g => g.DisposalId == null || g.DisposalId <= 0)
                .OrderBy(p => p.ColorId).ThenBy(g => g.Code).ThenBy(g => g.BirthDate)
                .ToListAsync(cancellationToken);
            
            return new GoatsListViewModel
            {

                Goats = goatsDto,
                CreateEnabled = true
            };
        }
    }
}
