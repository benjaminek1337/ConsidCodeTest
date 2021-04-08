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

        public async Task AddCategory(Category category)
        {
            await categories.Add(category);
        }

        public async Task UpdateCategory(Category category)
        {
            await categories.Update(category);
        }

        public async Task<bool> DeleteCategory(Category entry)
        {
            // Kanske går att kolla på entry.LibraryItems ist för att köra en select. Men ja.
            var entries = await libraryItems.Select(x => x.CategoryId == entry.Id);

            if (entries.ToList().Count > 0)
            {
                // Då kan vi ju inte radera
                return false;
            }
            // Men nu kan vi rocka loss
            //categories.Delete(entry);
            return true;
        }

        public async Task<List<Category>> GetCategories()
        {
            var allCategories = await categories.GetAll();
            return allCategories.ToList();
        }
    }
}
