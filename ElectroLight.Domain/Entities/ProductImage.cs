using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ElectroLight.Domain.Entities
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(500)]
        public string? ImageUrl { get; set; } = string.Empty;

        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;
    }
}
