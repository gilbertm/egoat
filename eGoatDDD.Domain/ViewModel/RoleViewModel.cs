using System.ComponentModel.DataAnnotations;

namespace eGoatDDD.Domain.ViewModel
{
    public class RoleViewModel
    {
        [Required]
        public string [] RoleData { get; set; }
        public string RoleOptions { get; set; }
    }
}
