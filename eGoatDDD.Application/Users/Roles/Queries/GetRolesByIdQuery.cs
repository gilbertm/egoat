using MediatR;
using eGoatDDD.Application.Users.Roles.Models;

namespace eGoatDDD.Application.Users.Roles.Queries
{
    public class GetRolesByIdQuery : IRequest<RolesUserViewModel>
    {
        public GetRolesByIdQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId;
    }
}
