using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace eGoatDDD.Persistence
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            // CreateMap<IEnumerable<RoleViewModel>, List<IdentityRole>>();
            // CreateMap<List<IdentityRole>, IEnumerable<UserRoleViewModel>>();
        }
    }
}
