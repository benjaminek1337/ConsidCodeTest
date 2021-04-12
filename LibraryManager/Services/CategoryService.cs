using LibraryManager.Models;
using LibraryManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Services
{
    public class CategoryService : ICategoryService
    {
        ILibraryDbRepository<Category> categories;
        ILibraryDbRepository<LibraryItem> libraryItems;

        public CategoryService(ILibraryDbRepository<Category> categories, ILibraryDbRepository<LibraryItem> libraryItems)
        {
            this.categories = categories;
            this.libraryItems = libraryItems;
        }

        public async Task<bool> AddCategoryAsync(Category category)
        {
            if(await categories.AnyAsync(x => x.CategoryName.ToLower() == category.CategoryName.ToLower()))
            {
                return false;
            }
            await categories.AddAsync(category);
            return true;
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            if(await categories.AnyAsync(x => x.CategoryName.ToLower() == category.CategoryName.ToLower()))
            {
                return false;
            }
            await categories.UpdateAsync(category);
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(Category entry)
        {
            if (await libraryItems.AnyAsync(x => x.CategoryId == entry.Id))
            {
                return false;
            }

            await categories.DeleteAsync(entry);
            return true;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await categories.GetByIdAsync(id);
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var allCategories = await categories.GetAllAsync();
            return allCategories.ToList();
        }

        public async Task<bool> CategoryExists(int id)
        {
            return await categories.AnyAsync(x => x.Id == id);
        }

    }
}
