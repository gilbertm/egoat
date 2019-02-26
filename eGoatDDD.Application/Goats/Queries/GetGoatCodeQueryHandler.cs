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
    public class GetGoatCodeQueryHandler : MediatR.IRequestHandler<GetGoatCodeQuery, bool>
    {
        private readonly eGoatDDDDbContext _context;

        public GetGoatCodeQueryHandler(eGoatDDDDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(GetGoatCodeQuery request, CancellationToken cancellationToken)
        {
            var goat = await _context.Goats
                .Select(GoatDto.Projection)
                .Where(g => g.ColorId == request.ColorId && g.Code == request.Code)
                .SingleOrDefaultAsync(cancellationToken);

            if (goat == null)
            {
                return true;
            }

            return false;
        }
    }
}
