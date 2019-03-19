using MediatR;
using eGoatDDD.Application.Users.Roles.Models;
using System.Collections.Generic;

namespace eGoatDDD.Application.Users.Roles.Queries
{
    public class GetRolesNameQuery : IRequest<IEnumerable<string>>
    {
        public GetRolesNameQuery()
        {

        }
    }
}
