using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using eGoatDDD.Persistence.Repository;
using Microsoft.AspNetCore.Identity;
using eGoatDDD.Application.Users.Manages.Models;
using AutoMapper;
using System.Collections.Generic;
using eGoatDDD.Persistence;
using System.Linq;
using eGoatDDD.Application.Users.Roles.Models;

namespace eGoatDDD.Application.Users.Manages.Queries
{
    public class GetUsersRolesQueryHandler : IRequestHandler<GetUsersRolesQuery, IEnumerable<UserRolesViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly eGoatDDDDbContext _context;


        public GetUsersRolesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, eGoatDDDDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<UserRolesViewModel>> Handle(GetUsersRolesQuery request, CancellationToken cancellationToken)
        {
            var userList = await (from user in _context.ApplicationUsers
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      user.Email,
                                      user.EmailConfirmed,
                                      RoleNames = (from aa in _context.UserRoles
                                                   join bb in _context.Roles
                                                   on aa.RoleId equals bb.Id into newgroup
                                                   from cc in newgroup.DefaultIfEmpty()
                                                   select cc).ToList()
                                  }).ToListAsync();

            var userRolesListViewModel = userList.Select(p => new UserRolesViewModel
            {
                UserId = p.UserId,
                UserName = p.Username,
                Email = p.Email,
                EmailConfirmed = p.EmailConfirmed,
                Roles = new SelectOptionList
                {
                    SelectOptionViewModels = _mapper.Map<IList<SelectOptionViewModel>>(p.RoleNames)
                }               
            });

            return userRolesListViewModel;
        }
    }
}
