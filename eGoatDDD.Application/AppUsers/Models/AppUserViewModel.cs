namespace eGoatDDD.Application.AppUsers.Models
{
    public class AppUserViewModel
    {
        public AppUserDto AppUser { get; set; }

        public bool EditEnabled { get; set; }

        public bool DeleteEnabled { get; set; }
    }
}
