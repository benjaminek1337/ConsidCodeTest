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

        public async Task AddCategoryAsync(Category category)
        {
            await categories.AddAsync(category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await categories.UpdateAsync(category);
        }

        public async Task<bool> DeleteCategoryAsync(Category entry)
        {
            // Kanske går att kolla på entry.LibraryItems ist för att köra en select. Men ja.
            var entries = await libraryItems.SelectAsync(x => x.CategoryId == entry.Id);

            if (entries.ToList().Count > 0)
            {
                // Då kan vi ju inte radera
                return false;
            }
            // Men nu kan vi rocka loss
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
            return await libraryItems.AnyAsync(x => x.Id == id);
        }

    }
}
