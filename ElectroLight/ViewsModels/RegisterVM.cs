using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ElectroLight.ViewsModels
{
    public class RegisterVM
    {

        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } 

        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        public string? ReturnUrl { get; set; } = null;


        public string? Role { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? Roles { get; set; }


    }
}
