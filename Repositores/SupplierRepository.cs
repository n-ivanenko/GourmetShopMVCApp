using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GourmetShopMVCApp.Models;

namespace GourmetShopMVCApp.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly GourmetShopContext _context;

        public SupplierRepository(GourmetShopContext context)
        {
            _context = context;
        }
        // Get all suppliers
        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }

        // Get all suppliers by ID
        public async Task<Supplier> GetSupplierByIdAsync(int id)
        {
            return await _context.Suppliers.FindAsync(id);
        }

        // Add a new supplier
        public async Task AddSupplierAsync(Supplier supplier)
        {
            await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync();
        }

        // Update an existing supplier
        public async Task UpdateSupplierAsync(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();
        }

        // Delete a supplier
        public async Task DeleteSupplierAsync(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
                await _context.SaveChangesAsync();
            }
        }

        // Search for a supplier by CompanyName or ContactName
        public async Task<IEnumerable<Supplier>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllSuppliersAsync();
            }

            return await _context.Suppliers
                .Where(s => s.CompanyName.Contains(searchTerm) || s.ContactName.Contains(searchTerm)) 
                .ToListAsync();
        }
    }
}
