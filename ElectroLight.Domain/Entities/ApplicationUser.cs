using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ElectroLight.Domain.Entities
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

    }
}
