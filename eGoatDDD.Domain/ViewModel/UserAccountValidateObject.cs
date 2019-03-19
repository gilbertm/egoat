using System.ComponentModel.DataAnnotations;

namespace eGoatDDD.Domain.ViewModel
{
    public class UserAccountValidateObject
    {
        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
