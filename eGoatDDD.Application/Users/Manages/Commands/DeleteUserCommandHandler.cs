using eGoatDDD.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using eGoatDDD.Persistence.Service.User;
using eGoatDDD.Persistence.Repository;
using AutoMapper;
using System.Transactions;
using Microsoft.AspNetCore.Identity;
using eGoatDDD.Application.Users.Roles.Queries;
using System.Collections.Generic;
using System.Security.Claims;

namespace eGoatDDD.Application.Users.Manages.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork, IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _userManager = userManager;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var roles = await _mediator.Send(new GetRolesQuery());

            IEnumerable<string> allRoles = (from r in roles.Roles
                                     select r.Name);
            ApplicationUser user = await _userManager.FindByEmailAsync(request.RolesUser.UserName);

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    foreach (var item in allRoles)
                    {
                        await _userManager.RemoveFromRoleAsync(user, item);

                        Claim claim = new Claim(ClaimTypes.Role, item);
                        await _userManager.RemoveClaimAsync(user, claim);
                    }

                    //Delete User
                    await _userManager.DeleteAsync(user);

                    transaction.Complete();
                }
                catch (Exception)
                {
                    _unitOfWork.Rollback();

                    return false;
                }
            }

            return true;
        }
    }
}