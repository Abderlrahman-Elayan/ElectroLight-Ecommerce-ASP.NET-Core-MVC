using ElectroLight.Domain.Entities;

namespace ElectroLight.ViewsModels
{
    public class HomeVM
    {
        public IEnumerable<Category> categoriesList;

        public IEnumerable<Product> newestProductsList;

        public IEnumerable<Product> featuredProductsList;

    }
}
