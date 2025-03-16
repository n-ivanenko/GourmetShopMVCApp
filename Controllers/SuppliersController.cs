using GourmetShopMVCApp.Models;
using GourmetShopMVCApp.Repositories;
using GourmetShopMVCApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GourmetShopMVCApp.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly ISupplierRepository _supplierRepository;

        public SuppliersController(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        // GET Suppliers
        public async Task<IActionResult> Index()
        {
            var suppliers = await _supplierRepository.GetAllSuppliersAsync();
            var viewModel = new SupplierSearchViewModel
            {
                SearchTerm = "",
                Suppliers = suppliers
            };
            return View(viewModel);
        }

        // GET Details
        public async Task<IActionResult> Details(int id)
        {
            var supplier = await _supplierRepository.GetSupplierByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET Create
        public IActionResult Create()
        {
            return View();
        }

        // POST Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyName,ContactName,ContactTitle,City,Country,Phone,Fax")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                await _supplierRepository.AddSupplierAsync(supplier);
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET Edit
        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await _supplierRepository.GetSupplierByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // POST Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyName,ContactName,ContactTitle,City,Country,Phone,Fax")] Supplier supplier)
        {
            if (id != supplier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _supplierRepository.UpdateSupplierAsync(supplier);
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET Delete
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _supplierRepository.GetSupplierByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _supplierRepository.DeleteSupplierAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // POST Search
        [HttpPost]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var suppliers = await _supplierRepository.SearchAsync(searchTerm);
            var viewModel = new SupplierSearchViewModel
            {
                SearchTerm = searchTerm,
                Suppliers = suppliers
            };
            return View("Index", viewModel);
        }
    }
}
