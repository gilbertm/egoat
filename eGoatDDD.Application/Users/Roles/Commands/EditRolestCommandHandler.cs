using eGoatDDD.Domain.Entities;
using eGoatDDD.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using eGoatDDD.Application.Users.Roles.Models;
using eGoatDDD.Persistence.Service.User;
using eGoatDDD.Persistence.Repository;
using AutoMapper;
using System.Transactions;

namespace eGoatDDD.Application.Users.Roles.Commands
{
    public class EditRolestCommandHandler : IRequestHandler<EditRolestCommand, bool>
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public EditRolestCommandHandler(IUserService userService, IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<bool> Handle(EditRolestCommand request, CancellationToken cancellationToken)
        {
            var userRoles = await _userService.GetUserRolesByGuid(request.RolesUser.UserId);
            var currentEditUser = await _userService.FindByIdAsync(request.RolesUser.UserId);

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _userService.RemoveFromRolesAsync(currentEditUser, userRoles.ToArray());
                    if (request.RolesUser.CurrentUserRoles.Any())
                    {
                        await _userService.AddUserToRolesAsync(currentEditUser, request.RolesUser.CurrentUserRoles.ToList());
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