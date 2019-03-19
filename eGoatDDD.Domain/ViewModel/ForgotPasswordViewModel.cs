using System.ComponentModel.DataAnnotations;

namespace eGoatDDD.Domain.ViewModel
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
