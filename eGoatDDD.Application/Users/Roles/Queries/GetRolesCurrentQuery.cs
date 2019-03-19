using MediatR;
using System.Collections.Generic;

namespace eGoatDDD.Application.Users.Roles.Queries
{
    public class GetRolesCurrentQuery : IRequest<IList<string>>
    {
        public GetRolesCurrentQuery()
        {

        }
    }
}
