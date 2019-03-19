using MediatR;
using eGoatDDD.Application.Users.Manages.Models;
using System.Collections.Generic;

namespace eGoatDDD.Application.Users.Manages.Queries
{
    public class GetUsersRolesQuery : IRequest<IEnumerable<UserRolesViewModel>>
    {
        public GetUsersRolesQuery()
        {

        }
    }
}
