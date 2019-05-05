using eGoatDDD.Application.Users.Manages.Models;
using MediatR;

namespace eGoatDDD.Application.Users.Manages.Commands
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public DeleteUserCommand(UserRolesViewModel userRolesViewModel)
        {
            RolesUser = userRolesViewModel;
        }

        public UserRolesViewModel RolesUser { get; set; }
    }
}