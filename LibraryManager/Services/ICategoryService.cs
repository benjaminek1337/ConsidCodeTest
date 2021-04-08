using LibraryManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Services
{
    public interface ICategoryService
    {
        Task<bool> DeleteCategory(Category entry);
        Task<List<Category>> GetCategories();
    }
}