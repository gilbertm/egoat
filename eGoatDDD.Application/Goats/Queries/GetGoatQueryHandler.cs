using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eGoatDDD.Application.Exceptions;
using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Persistence;
using eGoatDDD.Application.Goats.Queries;
using eGoatDDD.Domain.Entities;

namespace eGoatDDD.Application.Goats.Queries
{
    public class GetGoatQueryHandler : MediatR.IRequestHandler<GetGoatQuery, GoatViewModel>
    {
        private readonly eGoatDDDDbContext _context;

        public GetGoatQueryHandler(eGoatDDDDbContext context)
        {
            _context = context;
        }

        public async Task<GoatViewModel> Handle(GetGoatQuery request, CancellationToken cancellationToken)
        {
            var goat = await _context.Goats
                .Select(GoatDto.Projection)
                .Where(g => g.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (goat == null)
            {
                throw new NotFoundException(nameof(Goat), request.Id);
            }

            // TODO: Set view model state based on user permissions.
            var model = new GoatViewModel
            {
                Goat = goat,
                EditEnabled = true,
                DeleteEnabled = false
            };

            return model;
        }
    }
}
