using GourmetShopMVCApp.Models;

namespace GourmetShopMVCApp.ViewModels
{
    public class SupplierSearchViewModel
    {
        public string SearchTerm { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
