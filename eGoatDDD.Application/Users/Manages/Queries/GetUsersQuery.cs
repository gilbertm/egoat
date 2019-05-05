using eGoatDDD.Application.Users.Manages.Models;
using MediatR;
using System.Collections.Generic;

namespace eGoatDDD.Application.Users.Manages.Queries
{
    public class GetUsersQuery : IRequest<IEnumerable<UserRolesViewModel>>
    {

        public GetUsersQuery()
        {

        }

    }
}
