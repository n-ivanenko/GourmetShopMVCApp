using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GourmetShopMVCApp.Models;

namespace GourmetShopMVCApp.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly GourmetShopContext _context;

        public SuppliersController(GourmetShopContext context)
        {
            _context = context;
        }

        /// GET: Suppliers
        public async Task<IActionResult> Index()
        {
            try
            {
                var suppliers = await _context.Suppliers.ToListAsync();

                // Handle null values explicitly
                foreach (var supplier in suppliers)
                {
                    // Safely replace null values with defaults
                    supplier.ContactName = supplier.ContactName;
                    supplier.ContactTitle = supplier.ContactTitle;
                    supplier.Phone = supplier.Phone;
                    supplier.Fax = supplier.Fax;
                    supplier.City = supplier.City;
                    supplier.Country = supplier.Country;
                }

                return View(suppliers);
            }
            catch (Exception ex)
            {
                // Log the exception or return an error message
                Console.WriteLine($"Error: {ex.Message}");
                return View("Error");
            }
        }




        // GET: Suppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.Id == id);

            if (supplier == null)
            {
                return NotFound();
            }

            // Null handling: Ensure the properties are not null before accessing them
            if (supplier.ContactName == null)
            {
                supplier.ContactName = "No Contact Name Provided";
            }

            if (supplier.Phone == null)
            {
                supplier.Phone = "No Phone Provided";
            }

            // Return the view with the supplier model
            return View(supplier);
        }


        // GET: Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyName,ContactName,ContactTitle,City,Country,Phone,Fax")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }


        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Edit/5
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
                try
                {
                    _context.Update(supplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(int id)
        {
            return _context.Suppliers.Any(e => e.Id == id);
        }
    }
}
