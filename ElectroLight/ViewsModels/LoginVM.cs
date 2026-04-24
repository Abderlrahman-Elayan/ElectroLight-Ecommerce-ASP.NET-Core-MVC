using System.ComponentModel.DataAnnotations;

namespace ElectroLight.ViewsModels
{
    public class LoginVM
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; } = null;
        
    }
}
