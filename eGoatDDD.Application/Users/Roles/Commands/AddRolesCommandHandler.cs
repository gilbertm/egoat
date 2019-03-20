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
using Microsoft.AspNetCore.Identity;

namespace eGoatDDD.Application.Users.Manages.Commands
{
    public class AddRolestCommandHandler : IRequestHandler<AddRolesCommand, bool>
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AddRolestCommandHandler(IUserService userService, IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator, RoleManager<IdentityRole> roleManager)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
            _roleManager = roleManager;
        }

        public async Task<bool> Handle(AddRolesCommand request, CancellationToken cancellationToken)
        {
                foreach (var role in request.Roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new ApplicationRole
                        {
                            Name = role,
                            NormalizedName = role.ToUpper()
                        });
                    }
                }
           
            return true;
        }
    }
}