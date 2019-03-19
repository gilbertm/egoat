using eGoatDDD.Application.Users.Roles.Models;
using MediatR;

namespace eGoatDDD.Application.Users.Roles.Commands
{
    public class EditRolestCommand : IRequest<bool>
    {
        public EditRolestCommand(RolesUserViewModel rolesUser)
        {
            RolesUser = rolesUser;
        }

        public RolesUserViewModel RolesUser { get; set; }
    }
}