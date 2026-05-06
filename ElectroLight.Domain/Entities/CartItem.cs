using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElectroLight.Domain.Entities
{
    public class CartItem
    {

        public int Id { get; set; }

        public int ShoppingCartId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } 

        [ForeignKey("ShoppingCartId")]
        public ShoppingCart ShoppingCart { get; set; } = null!;

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;
    }
}
