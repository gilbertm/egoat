using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using eGoatDDD.Application.Breeds.Models;
using eGoatDDD.Persistence;

namespace eGoatDDD.Application.Breeds.Queries
{
    public class GetAlBreedsQueryHandler : IRequestHandler<GetAllBreedsQuery, BreedsListViewModel>
    {
        private readonly eGoatDDDDbContext _context;

        public GetAlBreedsQueryHandler(eGoatDDDDbContext context)
        {
            _context = context;
        }

        public async Task<BreedsListViewModel> Handle(GetAllBreedsQuery request, CancellationToken cancellationToken)
        {
            BreedsListViewModel model = new BreedsListViewModel
            {
                    Breeds = await _context.Breeds
                       .Select(BreedDto.Projection)
                       .OrderBy(b => b.Name)
                       .ToListAsync(cancellationToken),
                    CreateEnabled = true
                };

            

            return model;
        }
    }
}
