using eGoatDDD.Application.AppUsers.Models;
using eGoatDDD.Application.Exceptions;
using eGoatDDD.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eGoatDDD.Application.AppUsers.Queries
{
    public class GetAppUserQueryHandler : MediatR.IRequestHandler<GetAppUserQuery, AppUserViewModel>
    {
        private readonly eGoatDDDDbContext _context;

        public GetAppUserQueryHandler(eGoatDDDDbContext context)
        {
            _context = context;
        }

        public async Task<AppUserViewModel> Handle(GetAppUserQuery request, CancellationToken cancellationToken)
        {
            var AppUser = await _context.AppUsers
                .Where(l => l.Id == request.Id)
                .Select(AppUserDto.Projection)
                .SingleOrDefaultAsync(cancellationToken);

            if (AppUser == null)
            {
                throw new NotFoundException(nameof(AppUser), request.Id);
            }

            // TODO: Set view model state based on user permissions.
            var model = new AppUserViewModel
            {
                AppUser = AppUser,
                EditEnabled = true,
                DeleteEnabled = false
            };

            return model;
        }
    }
}
