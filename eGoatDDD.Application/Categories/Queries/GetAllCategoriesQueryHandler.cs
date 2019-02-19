using eGoatDDD.Application.Categories.Models;
using eGoatDDD.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eGoatDDD.Application.Categories.Queries
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, CategoriesListViewModel>
    {
        private readonly eGoatDDDDbContext _context;

        public GetAllCategoriesQueryHandler(eGoatDDDDbContext context)
        {
            _context = context;
        }

        public async Task<CategoriesListViewModel> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            // TODO: Set view model state based on user permissions.
            var model = new CategoriesListViewModel
            {
                Categories = await _context.Categories
                    .Select(CategoryDto.Projection)
                    .OrderBy(p => p.Name)
                    .ToListAsync(cancellationToken),
                CreateEnabled = true
            };

            return model;
        }
    }
}
