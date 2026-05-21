using ElectroLight.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectroLight.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; } = null!;

        public string Provider { get; set; } = "PayPal";

        public string TransactionId { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        public DateTime CreatedAt { get; set; }
    }

}
