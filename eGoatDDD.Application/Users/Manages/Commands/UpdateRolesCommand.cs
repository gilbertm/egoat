using eGoatDDD.Application.Users.Manages.Models;
using MediatR;

namespace eGoatDDD.Application.Users.Manages.Commands
{
    public class UpdateRolesCommand : IRequest<bool>
    {
        public UpdateRolesCommand(UserRolesViewModel userRolesViewModel)
        {
            RolesUser = userRolesViewModel;
        }

        public UserRolesViewModel RolesUser { get; set; }
    }
}