using System.Collections.Generic;

namespace eGoatDDD.Application.AppUsers.Models
{
    public class AppUsersListViewModel
    {
        public IEnumerable<AppUserDto> AppUsers { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
