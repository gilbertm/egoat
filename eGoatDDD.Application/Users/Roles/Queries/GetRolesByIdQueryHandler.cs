using System.Threading;
using System.Threading.Tasks;
using MediatR;
using eGoatDDD.Persistence.Repository;
using eGoatDDD.Application.Users.Roles.Models;
using AutoMapper;
using eGoatDDD.Persistence.Service.User;

namespace eGoatDDD.Application.Users.Roles.Queries
{
    public class GetRolesByIdQueryHandler : IRequestHandler<GetRolesByIdQuery, RolesUserViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUserService _userService;


        public GetRolesByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
            _userService = userService;
        }

        public async Task<RolesUserViewModel> Handle(GetRolesByIdQuery request, CancellationToken cancellationToken)
        {
            var userRoles = await _userService.GetUserRolesByGuid(request.UserId);

            var roles = await _mediator.Send(new GetRolesNameQuery());

            return new RolesUserViewModel
            {
                UserId = request.UserId,
                CurrentUserRoles = userRoles,
                RolesName = roles
            };

        }
    }
}
