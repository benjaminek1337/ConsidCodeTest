using LibraryManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Services
{
    public interface ICategoryService
    {
        Task<bool> AddCategoryAsync(Category category);
        Task<bool> CategoryExists(int id);
        Task<bool> DeleteCategoryAsync(Category entry);
        Task<List<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<bool> UpdateCategoryAsync(Category category);
    }
}