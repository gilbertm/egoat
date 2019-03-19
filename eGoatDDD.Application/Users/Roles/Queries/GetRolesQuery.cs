using MediatR;
using eGoatDDD.Application.Users.Roles.Models;

namespace eGoatDDD.Application.Users.Roles.Queries
{
    public class GetRolesQuery : IRequest<UserRolesListViewModel>
    {
        public GetRolesQuery()
        {

        }
    }
}
