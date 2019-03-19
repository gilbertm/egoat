using System.ComponentModel.DataAnnotations;

namespace eGoatDDD.Domain.ViewModel
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
