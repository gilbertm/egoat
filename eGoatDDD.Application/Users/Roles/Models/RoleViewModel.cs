using System.ComponentModel.DataAnnotations;

namespace eGoatDDD.Application.Users.Roles.Models
{
    public class RoleViewModel
    {
        [Required]
        public string [] RoleData { get; set; }
        public string RoleOptions { get; set; }
    }
}
