using GourmetShopMVCApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GourmetShopMVCApp.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
    }
}
