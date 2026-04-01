using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;

namespace ElectroLight.Domain.Entities
{
    public class Category
    {
        [Key]
        public int Id{ get; set; }

        [StringLength(100,MinimumLength =3)]
        [Required]
        public string Name{ get; set; } =string.Empty;


        [MaxLength(100)]
        public string? Description { get; set; }

        [MaxLength(500)]
        public string? ImageUrl { get; set; } = "/img/placeholder.jpg";

        [NotMapped]
        public IFormFile? Image { get; set; }


        [ValidateNever]
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
    