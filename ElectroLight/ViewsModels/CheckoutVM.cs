using ElectroLight.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ElectroLight.ViewsModels
{
    public class CheckoutVM
    {
        public string Address { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        [ValidateNever]
        public ShoppingCart Cart { get; set; } = null!;
    }
}
