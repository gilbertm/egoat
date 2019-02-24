using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eGoatDDD.Application.Exceptions;
using eGoatDDD.Application.Colors.Models;
using eGoatDDD.Persistence;
using eGoatDDD.Domain.Entities;

namespace eGoatDDD.Application.Colors.Queries
{
    public class GetColorQueryHandler : MediatR.IRequestHandler<GetColorQuery, ColorViewModel>
    {
        private readonly eGoatDDDDbContext _context;

        public GetColorQueryHandler(eGoatDDDDbContext context)
        {
            _context = context;
        }

        public async Task<ColorViewModel> Handle(GetColorQuery request, CancellationToken cancellationToken)
        {
            var Color = await _context.Colors
                .Select(ColorDto.Projection)
                .Where(b => b.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (Color == null)
            {
                throw new NotFoundException(nameof(Color), request.Id);
            }

            // TODO: Set view model state based on user permissions.
            var model = new ColorViewModel
            {
                Color = Color,
                EditEnabled = true,
                DeleteEnabled = false
            };

            return model;
        }
    }
}
