using eGoatDDD.Application.Users.Manages.Models;
using eGoatDDD.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using eGoatDDD.Application.Users.Roles.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace eGoatDDD.Application.Users.Manages.Queries
{
    class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserRolesViewModel>>
    {
        private readonly eGoatDDDDbContext _context;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IMapper mapper, eGoatDDDDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserRolesViewModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
           
            var userRolesListViewModel = new List<UserRolesViewModel>();

            foreach (var item in _context.ApplicationUsers)
            {
                var userRoles = from uroles in _context.UserRoles.Where(u => u.UserId.Equals (item.Id))
                                 select uroles;

                var userRolesTransformToNames = await (from aa in _context.Roles
                                 join bb in userRoles
                                 on aa.Id equals bb.RoleId into newgroup
                                 from cc in newgroup.DefaultIfEmpty()
                                 select new
                                 {
                                     Label = aa.Name,
                                     Value = (cc.UserId == item.Id ? true : false)
                                 }).ToListAsync();

                userRolesListViewModel.Add(
                    new UserRolesViewModel
                    {
                        UserId = item.Id,
                        UserName = item.UserName,
                        Email = item.Email,
                        EmailConfirmed = item.EmailConfirmed,
                        Roles = new SelectOptionList
                        {
                            SelectOptionViewModels = _mapper.Map<IList<SelectOptionViewModel>>(userRolesTransformToNames)
                        }
                    }
                );
            }

            return userRolesListViewModel;

        }

    }
}
