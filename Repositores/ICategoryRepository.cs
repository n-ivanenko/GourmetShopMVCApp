using GourmetShopMVCApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GourmetShopMVCApp.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
    }
}
