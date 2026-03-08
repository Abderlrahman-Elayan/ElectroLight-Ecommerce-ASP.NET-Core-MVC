using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElectroLight.Domain.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, MinimumLength = 3)]
        public string? Description { get; set; }

        [Required]
        [Range(0.01, 100000)]
        //[Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [StringLength(500, MinimumLength = 3)]
        public string? ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } 
    }
}
