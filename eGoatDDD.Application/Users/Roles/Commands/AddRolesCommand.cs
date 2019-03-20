using eGoatDDD.Application.Users.Manages.Models;
using MediatR;
using System.Collections.Generic;

namespace eGoatDDD.Application.Users.Manages.Commands
{
    public class AddRolesCommand : IRequest<bool>
    {
        public AddRolesCommand(IEnumerable<string> roles)
        {
            Roles = roles;
        }

        public IEnumerable<string> Roles { get; set; }
    }
}