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

        /// <summary>
        /// Adds a category to the database if a category with the same name doesnt exist
        /// </summary>
        /// <param name="category">The category object</param>
        /// <returns>A bool indicating whether or not the object could be added</returns>
        public async Task<bool> AddCategoryAsync(Category category)
        {
            if(await categories.AnyAsync(x => x.CategoryName.ToLower() == category.CategoryName.ToLower()))
            {
                return false;
            }
            await categories.AddAsync(category);
            return true;
        }

        /// <summary>
        /// Updates a category in the database, provided that there doesnt exist a category with the same name
        /// </summary>
        /// <param name="category">The category object</param>
        /// <returns>A bool indicating whether or not the object could be updated</returns>
        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            if(await categories.AnyAsync(x => x.CategoryName.ToLower() == category.CategoryName.ToLower()))
            {
                return false;
            }
            await categories.UpdateAsync(category);
            return true;
        }

        /// <summary>
        /// Deletes a category from the database, provided that a libraryItem doesnt reference it
        /// </summary>
        /// <param name="category">The category object</param>
        /// <returns>A bool indicating whether or not the operation was successful</returns>
        public async Task<bool> DeleteCategoryAsync(Category category)
        {
            if (await libraryItems.AnyAsync(x => x.CategoryId == category.Id))
            {
                return false;
            }

            await categories.DeleteAsync(category);
            return true;
        }

        /// <summary>
        /// Gets a category from the database, by the categorys Id value
        /// </summary>
        /// <param name="id">The categorys Id value</param>
        /// <returns>A Category object</returns>
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await categories.GetByIdAsync(id);
        }

        /// <summary>
        /// Gets all categories from the database
        /// </summary>
        /// <returns>A list of Categories</returns>
        public async Task<List<Category>> GetCategoriesAsync()
        {
            var allCategories = await categories.GetAllAsync();
            return allCategories.ToList();
        }

        /// <summary>
        /// Checks whether or not a category field exists in the database
        /// </summary>
        /// <param name="id">The id to compare against the database</param>
        /// <returns>True or false if the field exists or not</returns>
        public async Task<bool> CategoryExists(int id)
        {
            return await categories.AnyAsync(x => x.Id == id);
        }

    }
}
