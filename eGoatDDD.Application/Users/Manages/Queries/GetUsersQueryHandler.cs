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
            var userList = await (from user in _context.ApplicationUsers
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      user.Email,
                                      user.EmailConfirmed,
                                      RoleNames = (from aa in _context.Roles
                                                   join bb in _context.UserRoles.Where(u => u.UserId.Equals(user.Id))
                                                   on aa.Id equals bb.RoleId into newgroup
                                                   from cc in newgroup.DefaultIfEmpty()
                                                   select new
                                                   {
                                                       Label = aa.Name,
                                                       Value = (cc.UserId == null ? false : true)
                                                   }).ToList()
                                  }).ToListAsync();

            var userRolesListViewModel = userList.Select(p => new UserRolesViewModel
            {
                UserId = p.UserId,
                UserName = p.Username,
                Email = p.Email,
                EmailConfirmed = p.EmailConfirmed.ToString(),

                Roles = new SelectOptionList
                {
                    SelectOptionViewModels = _mapper.Map<IList<SelectOptionViewModel>>(p.RoleNames)
                }
            });

            return userRolesListViewModel;

        }

    }
}
