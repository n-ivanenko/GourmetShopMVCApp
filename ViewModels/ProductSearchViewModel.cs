using GourmetShopMVCApp.Models;

namespace GourmetShopMVCApp.ViewModels
{
    public class ProductSearchViewModel
    {
        public string SearchTerm { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
    }
}

