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
    public class GetAllGoatsPotentialParentQueryHandler : IRequestHandler<GetAllGoatsPotentialParentQuery, GoatsListViewModel>
    {
        private readonly eGoatDDDDbContext _context;

        public GetAllGoatsPotentialParentQueryHandler(eGoatDDDDbContext context)
        {
            _context = context;
        }

        public async Task<GoatsListViewModel> Handle(GetAllGoatsPotentialParentQuery request, CancellationToken cancellationToken)
        {
            var goats = await _context.Goats
                .Include(c => c.Color)
                .Include(d => d.Disposal)
                .Include(gb => gb.GoatBreeds)
                .ThenInclude(b => b.Breed)
                .Include(p => p.Parents)
                .Include(gr => gr.GoatResources)
                .ThenInclude(r => r.Resource)
                .Select(resource => GoatDto.Create(resource)).ToListAsync(cancellationToken);

            GoatsListViewModel model = new GoatsListViewModel
            {
                Goats = goats.Where(g => g.DisposalId == null || g.DisposalId <= 0)
                .Where(g => g.Gender == (request.IsSire == true ? 'M' : 'F'))
                .OrderBy(p => p.ColorId).ThenBy(g => g.Code).ThenBy(g => g.BirthDate)
                .Where(g => this.Years(g.BirthDate, DateTime.Now) > 0)
                .ToList(),

                CreateEnabled = true
            };
            return model;
        }

        private int Years(DateTime? start, DateTime end)
        {
            if (start.HasValue)
            {
                return (end.Year - start.Value.Year - 1) +
                (((end.Month > start.Value.Month) ||
                ((end.Month == start.Value.Month) && (end.Day >= start.Value.Day))) ? 1 : 0);
            }

            return 0;
        }
    }
}
