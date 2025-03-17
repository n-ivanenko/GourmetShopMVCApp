using GourmetShopMVCApp.Models;
using GourmetShopMVCApp.Repositories;
using GourmetShopMVCApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace GourmetShopMVCApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductsController(IProductRepository productRepository, ISupplierRepository supplierRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;
            _categoryRepository = categoryRepository;
        }

        // GET Products
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync(); 
            var categories = await _categoryRepository.GetAllAsync(); 
            var viewModel = new ProductSearchViewModel
            {
                SearchTerm = "", 
                Products = products
            };
            ViewData["Categories"] = new SelectList(categories, "Id", "CategoryName");
            return View(viewModel);
        }

        // GET Details
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var suppliers = await _supplierRepository.GetAllSuppliersAsync();
            var categories = await _categoryRepository.GetAllAsync();

            ViewData["Suppliers"] = new SelectList(suppliers, "Id", "CompanyName");
            ViewData["Categories"] = new SelectList(categories, "Id", "CategoryName");

            return View();
        }

        // POST Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductName, SupplierId, UnitPrice, Package, IsDiscontinued, CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }

            var suppliers = await _supplierRepository.GetAllSuppliersAsync();
            var categories = await _categoryRepository.GetAllAsync();

            ViewData["Suppliers"] = new SelectList(suppliers, "Id", "CompanyName", product.SupplierId);
            ViewData["Categories"] = new SelectList(categories, "Id", "CategoryName", product.CategoryId);

            return View(product);
        }

        // GET Edit
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var suppliers = await _supplierRepository.GetAllSuppliersAsync();
            var categories = await _categoryRepository.GetAllAsync();

            ViewData["Suppliers"] = new SelectList(suppliers, "Id", "CompanyName", product.SupplierId);
            ViewData["Categories"] = new SelectList(categories, "Id", "CategoryName", product.CategoryId);
            ViewData["Title"] = "Edit Product";

            return View(product);
        }

        // POST Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, ProductName, SupplierId, UnitPrice, CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.UpdateAsync(product);
                return RedirectToAction(nameof(Index));
            }

            var suppliers = await _supplierRepository.GetAllSuppliersAsync();
            var categories = await _categoryRepository.GetAllAsync();

            ViewData["Suppliers"] = new SelectList(suppliers, "Id", "CompanyName", product.SupplierId);
            ViewData["Categories"] = new SelectList(categories, "Id", "CategoryName", product.CategoryId);

            return RedirectToAction("Index");
        }

        // GET Delete
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET Create Category
        public IActionResult CreateCategory(int productId)
        {
            var product = _productRepository.GetByIdAsync(productId).Result;

            ViewData["ProductName"] = product?.ProductName;

            return View();
        }

        // POST Create Category
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.AddAsync(category);
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // POST Search
        [HttpPost]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var products = await _productRepository.SearchAsync(searchTerm);
            var viewModel = new ProductSearchViewModel
            {
                SearchTerm = searchTerm,
                Products = products
            };
            return View("Index", viewModel);
        }
    }
}

