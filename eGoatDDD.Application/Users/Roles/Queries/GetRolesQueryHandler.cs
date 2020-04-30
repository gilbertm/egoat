using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using eGoatDDD.Persistence.Repository;
using Microsoft.AspNetCore.Identity;
using eGoatDDD.Application.Users.Roles.Models;
using System.Linq;

namespace eGoatDDD.Application.Users.Roles.Queries
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, RolesListViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;


        public GetRolesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RolesListViewModel> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _unitOfWork.Repository<IdentityRole>().Query().ToListAsync();

            return new RolesListViewModel
            {
                CreateEnabled = true,
                Roles = roles.Select(r => new RoleViewModel
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToList(),
            };
        }
    }
}
