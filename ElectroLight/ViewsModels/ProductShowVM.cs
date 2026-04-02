using ElectroLight.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElectroLight.ViewsModels
{
    public class ProductShowVM
    {

        [ValidateNever]
        public IEnumerable<Category> CategoriesList { get; set; } = null!;

        [ValidateNever]
        public string? CategoryName { get; set; } = string.Empty;

        [ValidateNever]
        public IEnumerable<Product> ProductsList { get; set; } = null!;
    }
}
