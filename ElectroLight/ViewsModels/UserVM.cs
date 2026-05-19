using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroLight.ViewsModels
{
    public class UserVM
    {
        public string Id{ get; set; }

        [Display(Name = "Full Name")]
        [Required]
        public string FullName { get; set; }

        [EmailAddress]
        [Required]
        public string Email{ get; set; }
        [Display (Name="Phone Number")]
        [Phone]
        [Required]
        public string PhoneNumber{ get; set; }

        [Required(ErrorMessage = "Please select a role")]
        public string Role{ get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> Roles { get; set; }

    }
}
