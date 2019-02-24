using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using eGoatDDD.Application.Colors.Models;
using eGoatDDD.Persistence;

namespace eGoatDDD.Application.Colors.Queries
{
    public class GetAlColorsQueryHandler : IRequestHandler<GetAllColorsQuery, ColorsListViewModel>
    {
        private readonly eGoatDDDDbContext _context;

        public GetAlColorsQueryHandler(eGoatDDDDbContext context)
        {
            _context = context;
        }

        public async Task<ColorsListViewModel> Handle(GetAllColorsQuery request, CancellationToken cancellationToken)
        {
            ColorsListViewModel model = new ColorsListViewModel
            {
                    Colors = await _context.Colors
                       .Select(ColorDto.Projection)
                       .OrderBy(b => b.Name)
                       .ToListAsync(cancellationToken),
                    CreateEnabled = true
                };

            

            return model;
        }
    }
}
