using eGoatDDD.Application.Users.Manages.Models;
using eGoatDDD.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using eGoatDDD.Application.Users.Roles.Models;
using Microsoft.EntityFrameworkCore;

namespace eGoatDDD.Application.Users.Manages.Queries
{
    class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserRolesViewModel>>
    {
        private readonly eGoatDDDDbContext _context;

        public GetUsersQueryHandler(eGoatDDDDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserRolesViewModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {

            var userRolesListViewModel = new List<UserRolesViewModel>();

            var applicationUsers = await _context.ApplicationUsers.ToListAsync();


            foreach (var item in applicationUsers)
            {

                var roles = await _context.Roles.ToListAsync();

                var userRolesTransformToNames = new List<SelectOptionViewModel>(); 
                
                foreach (var role in roles)
                {
                    var userRole = _context.UserRoles.Where(u => u.UserId.Equals(item.Id) && u.RoleId.Equals(role.Id)).FirstOrDefault();


                    userRolesTransformToNames.Add(new SelectOptionViewModel
                    {
                        Label = role.Name,
                        Value = userRole is { } ? true : false
                    });
                };

                userRolesListViewModel.Add(
                    new UserRolesViewModel
                    {
                        UserId = item.Id,

                        UserName = item.UserName,
                        Email = item.Email,
                        EmailConfirmed = item.EmailConfirmed,
                        Roles = new SelectOptionList
                        {
                            SelectOptionViewModels = userRolesTransformToNames
                        }
                    }
                );
            }

            return userRolesListViewModel;

        }

    }
}
