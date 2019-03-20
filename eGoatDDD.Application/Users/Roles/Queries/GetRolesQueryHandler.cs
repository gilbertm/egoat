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
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, RolesListViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public GetRolesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RolesListViewModel> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _unitOfWork.Repository<IdentityRole>().Query().ToListAsync();

            var rolesMapper = _mapper.Map<IEnumerable<RoleViewModel>>(roles);

            return new RolesListViewModel
            {
                CreateEnabled = true,
                Roles = rolesMapper
            };
        }
    }
}
