using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using eGoatDDD.Persistence.Repository;
using Microsoft.AspNetCore.Identity;
using eGoatDDD.Application.Users.Roles.Models;
using AutoMapper;
using System.Collections.Generic;

namespace eGoatDDD.Application.Users.Roles.Queries
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, UserRolesListViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public GetRolesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserRolesListViewModel> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var userRoles = await _unitOfWork.Repository<IdentityRole>().Query().ToListAsync();

            var userRolesMapper = _mapper.Map<IEnumerable<UserRoleViewModel>>(userRoles);

            return new UserRolesListViewModel
            {
                CreateEnabled = true,
                UserRoles = userRolesMapper
            };
        }
    }
}
