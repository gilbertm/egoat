using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eGoatDDD.Application.Exceptions;
using eGoatDDD.Application.Breeds.Models;
using eGoatDDD.Persistence;
using eGoatDDD.Domain.Entities;

namespace eGoatDDD.Application.Breeds.Queries
{
    public class GetBreedQueryHandler : MediatR.IRequestHandler<GetBreedQuery, BreedViewModel>
    {
        private readonly eGoatDDDDbContext _context;

        public GetBreedQueryHandler(eGoatDDDDbContext context)
        {
            _context = context;
        }

        public async Task<BreedViewModel> Handle(GetBreedQuery request, CancellationToken cancellationToken)
        {
            var breed = await _context.Breeds
                .Select(BreedDto.Projection)
                .Where(b => b.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (breed == null)
            {
                throw new NotFoundException(nameof(Breed), request.Id);
            }

            // TODO: Set view model state based on user permissions.
            var model = new BreedViewModel
            {
                Breed = breed,
                EditEnabled = true,
                DeleteEnabled = false
            };

            return model;
        }
    }
}
