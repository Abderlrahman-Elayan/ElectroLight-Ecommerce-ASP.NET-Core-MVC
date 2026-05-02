using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElectroLight.Domain.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<CartItem> cartItems { get; set; } = new List<CartItem>();

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

    }
}
