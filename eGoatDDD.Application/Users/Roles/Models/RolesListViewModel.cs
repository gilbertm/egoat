using System.Collections.Generic;

namespace eGoatDDD.Application.Users.Roles.Models
{
    public class RolesListViewModel
    {
        public IEnumerable<RoleViewModel> Roles { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
