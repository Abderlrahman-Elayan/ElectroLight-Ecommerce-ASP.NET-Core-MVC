using ElectroLight.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElectroLight.ViewsModels
{
    public class ProductVM
    {
        public Product Product { get; set; } = null!;

        [ValidateNever]
        public IEnumerable<SelectListItem> CategoriesList { get; set; } = null!;

      

    }
}
