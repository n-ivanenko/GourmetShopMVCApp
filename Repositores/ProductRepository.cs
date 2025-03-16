using GourmetShopMVCApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GourmetShopMVCApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly GourmetShopContext _context;

        public ProductRepository(GourmetShopContext context)
        {
            _context = context;
        }

        // Get all products
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            {
                return await _context.Products
                                     .Include(p => p.Supplier)
                                     .ToListAsync(); 
            }
        }

        // Get a product by ID
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.Include(p => p.Supplier).FirstOrDefaultAsync(p => p.Id == id);
        }

        // Add a new product
        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        // Update an existing product
        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        // Delete a product by ID
        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
