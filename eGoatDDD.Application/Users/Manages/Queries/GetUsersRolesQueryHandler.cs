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
                                      RoleNames = (from userRole in _context.UserRoles  //[AspNetUserRoles]
                                                   where userRole.UserId.Equals(user.Id)
                                                   join role in _context.Roles          //[AspNetRoles]//
                                                   on userRole.RoleId
                                                   equals role.Id
                                                   select new
                                                   {
                                                       Value = role.Name,
                                                       Label = role.Name
                                                   }).ToList()
                                  }).ToListAsync();

            var userRolesListViewModel = userList.Select(p => new UserRolesViewModel
            {
                UserId = p.UserId,
                UserName = p.Username,
                Email = p.Email,
                Roles = new SelectOptionList
                {
                    SelectOptionViewModels = _mapper.Map<IEnumerable<SelectOptionViewModel>>(p.RoleNames)
                },
                EmailConfirmed = p.EmailConfirmed.ToString()
            });

            //  _mapper.Map<IEnumerable<UserRoleViewModel>>(userRoles);

            return userRolesListViewModel;
        }
    }
}
