using eGoatDDD.Application.Exceptions;
using eGoatDDD.Application.AppUsers.Models;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace eGoatDDD.Application.AppUsers.Commands
{
    public class UpdateAppUserCommandHandler : IRequestHandler<UpdateAppUserCommand, AppUserDto>
    {
        private readonly eGoatDDDDbContext _context;

        public UpdateAppUserCommandHandler(eGoatDDDDbContext context)
        {
            _context = context;
        }

        public async Task<AppUserDto> Handle(UpdateAppUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.AppUsers
                .FindAsync(request.AppUserId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(AppUser), request.AppUserId);
            }

            entity.Role = request.Role;
            entity.FirstName = request.FirstName;
            entity.MiddleName= request.MiddleName;
            entity.LastName = request.LastName;
            entity.HomeCity= request.HomeAddress;
            entity.HomeAddress= request.HomeAddress;
            entity.HomeRegion = request.HomeRegion;
            entity.HomeCountryCode = request.HomeCountryCode;
            entity.HomePhone = request.HomePhone;
            entity.IsActivated = request.IsActivated;
            entity.Joined = request.Joined;

            await _context.SaveChangesAsync(cancellationToken);

            return AppUserDto.Create(entity);
        }
    }
}
