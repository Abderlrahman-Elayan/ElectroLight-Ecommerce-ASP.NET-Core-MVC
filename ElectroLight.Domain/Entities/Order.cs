using System;
using System.Collections.Generic;
using System.Text;

namespace ElectroLight.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public decimal TotalPrice { get; set; }

        public string Status { get; set; } = "Pending";

        public string Address { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
