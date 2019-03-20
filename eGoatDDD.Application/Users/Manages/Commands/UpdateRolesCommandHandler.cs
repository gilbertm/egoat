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
    public class UpdateRolesCommandHandler : IRequestHandler<UpdateRolesCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateRolesCommandHandler(IUnitOfWork unitOfWork, IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _userManager = userManager;
        }

        public async Task<bool> Handle(UpdateRolesCommand request, CancellationToken cancellationToken)
        {
            var roles = await _mediator.Send(new GetRolesQuery());

            IEnumerable<string> allRoles = (from r in roles.Roles
                                     select r.Name);

            IEnumerable<string> newRoles = (from uroles in request.RolesUser.Roles.SelectOptionViewModels.Where(r => r.Value == true)
                                     select  uroles.Label);
            
            ApplicationUser user = await _userManager.FindByIdAsync(request.RolesUser.UserId);

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

                    foreach (var item in newRoles)
                    {
                        await _userManager.AddToRoleAsync(user, item);

                        Claim claim = new Claim(ClaimTypes.Role, item);
                        await _userManager.AddClaimAsync(user, claim);
                    }

                    transaction.Complete();
                }
                catch (Exception)
                {
                    _unitOfWork.Rollback();
                }
            }

            return true;
        }
    }
}