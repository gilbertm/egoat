using eGoatDDD.Application.AppUsers.Models;
using eGoatDDD.Application.AppUsers.Queries;
using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace eGoatDDD.Application.AppUsers.Commands
{
    public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommand, AppUserViewModel>
    {
        private readonly eGoatDDDDbContext _context;
        private readonly IMediator _mediator;

        public CreateAppUserCommandHandler(
            eGoatDDDDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<AppUserViewModel> Handle(CreateAppUserCommand request, CancellationToken cancellationToken)
        {
            var entity = new AppUser
            {
                Role = request.Role,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                HomeAddress = request.HomeAddress,
                HomeCity = request.HomeCity,
                HomeRegion = request.HomeRegion,
                HomeCountryCode = request.HomeCountryCode,
                HomePhone = request.HomePhone,
                IsActivated = request.IsActivated,
                Joined = request.Joined,
                PackageId = request.PackageId,

            };

            _context.AppUsers.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return await _mediator.Send(new GetAppUserQuery(entity.Id), cancellationToken);
        }
    }
}