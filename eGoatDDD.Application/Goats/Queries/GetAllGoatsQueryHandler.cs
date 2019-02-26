using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Persistence;

namespace eGoatDDD.Application.Goats.Queries
{
    public class GetAllGoatsQueryHandler : IRequestHandler<GetAllGoatsQuery, GoatsListViewModel>
    {
        private readonly eGoatDDDDbContext _context;

        public GetAllGoatsQueryHandler(eGoatDDDDbContext context)
        {
            _context = context;
        }

        public async Task<GoatsListViewModel> Handle(GetAllGoatsQuery request, CancellationToken cancellationToken)
        {
            GoatsListViewModel model = new GoatsListViewModel
            {
                    Goats = await _context.Goats
                       .Select(GoatDto.Projection)
                       .Where(g => g.DisposalId == null || g.DisposalId <= 0)
                       .OrderBy(p => p.ColorId).ThenBy(g => g.Code).ThenBy(g => g.BirthDate)
                       .ToListAsync(cancellationToken),
                    CreateEnabled = true
                };

            

            return model;
        }
    }
}
