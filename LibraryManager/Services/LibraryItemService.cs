using LibraryManager.Models;
using LibraryManager.Models.ViewModels;
using LibraryManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Services
{
    public class LibraryItemService : ILibraryItemService
    {
        ILibraryDbRepository<LibraryItem> libraryItems;
        ILibraryDbRepository<Category> categories;
        public LibraryItemService(ILibraryDbRepository<LibraryItem> libraryItems, ILibraryDbRepository<Category> categories)
        {
            this.libraryItems = libraryItems;
            this.categories = categories;
        }

        /// <summary>
        /// Delete a LibraryItem field from the database
        /// </summary>
        /// <param name="id">The Id value to compare against the database table</param>
        public async Task DeleteItemAsync(int id)
        {
            var item = await GetItemByIdAsync(id);
            await libraryItems.DeleteAsync(item);
        }

        /// <summary>
        /// Adds a library item to the database
        /// </summary>
        /// <param name="model">The ViewModel containing the library item data</param>
        public async Task AddItemAsync(CreateEditLibraryItemViewModel model)
        {
            var item = await CreateLibraryItem(model);
            await libraryItems.AddAsync(item);
        }

        /// <summary>
        /// Updates a library item in the database
        /// </summary>
        /// <param name="model">The ViewModel containing the library item data</param>
        public async Task UpdateItemAsync(CreateEditLibraryItemViewModel model)
        {
            var item = await CreateLibraryItem(model);
            await libraryItems.UpdateAsync(item);
        }

        /// <summary>
        /// Updates a library item with a borrower and the DateTime when the transaction was completed
        /// </summary>
        /// <param name="model">The ViewModel containing the library item data</param>
        /// <returns></returns>
        public async Task BorrowItemAsync(BorrowItemViewModel model)
        {
            var item = await libraryItems.GetByIdAsync(model.Id);
            item.Borrower = model.Borrower;
            item.BorrowDate = DateTime.Now;

            await libraryItems.UpdateAsync(item);
        }

        /// <summary>
        /// Updates a library item, which nulls the Borrower and BorrowDate properties, signifying a returned item
        /// </summary>
        /// <param name="id">The Id of the item to update</param>
        public async Task ReturnItemAsync(int id)
        {
            var item = await libraryItems.GetByIdAsync(id);
            item.Borrower = null;
            item.BorrowDate = null;

            await libraryItems.UpdateAsync(item);
        }

        /// <summary>
        /// Gets the available library items, category data included
        /// </summary>
        /// <returns>A list of LibraryItems</returns>
        public async Task<List<LibraryItem>> GetAvailableItemsAsync()
        {
            var allLibraryItems = await libraryItems.GetAllAsync(x => x.Category);
            return allLibraryItems.ToList();
        }

        /// <summary>
        /// Gets a library item by an Id value
        /// </summary>
        /// <param name="id">The Id value to match with the database</param>
        /// <returns>A LibraryItem object</returns>
        public async Task<LibraryItem> GetItemByIdAsync(int id)
        {
            return await libraryItems.GetByIdAsync(id);
        }

        /// <summary>
        /// Method to fetch library items from the database, matching a query string
        /// </summary>
        /// <param name="query">The query string to compare against the database table</param>
        /// <returns>A list of LibraryItems</returns>
        public async Task<List<LibraryItem>> SearchItemsAsync(string query)
        {
            var allLibraryItems = await libraryItems.WhereAsync(x => x.Title == query || x.Author == query);
            return allLibraryItems.ToList();
        }

        /// <summary>
        /// Checks if an item exists in the database
        /// </summary>
        /// <param name="id">The Id to compare against the database</param>
        /// <returns>True or false, depending if an item is found</returns>
        public async Task<bool> ItemExistsAsync(int id)
        {
            return await libraryItems.AnyAsync(x => x.Id == id);
        }

        /// <summary>
        /// Method to create a LibraryItem object
        /// </summary>
        /// <param name="model">The ViewModel to map the data from</param>
        /// <returns>a LibraryItem object</returns>
        private async Task<LibraryItem> CreateLibraryItem(CreateEditLibraryItemViewModel model)
        {
            var item = new LibraryItem
            {
                Id = model.Id,
                Title = model.Title,
                Author = model.Author,
                Pages = model.Pages,
                RunTimeMinutes = model.RunTimeMinutes,
                Type = model.Type,
                IsBorrowable = model.Type == "Reference Book" ? false : true,
                Category = await categories.GetByIdAsync(model.CategoryId),
                CategoryId = model.CategoryId
            };

            return item;
        }

    }
}
