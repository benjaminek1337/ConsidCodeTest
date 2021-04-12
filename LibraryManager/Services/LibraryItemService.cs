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

        public async Task DeleteItemAsync(int id)
        {
            var item = await GetItemByIdAsync(id);
            await libraryItems.DeleteAsync(item);
        }

        public async Task AddItemAsync(CreateEditLibraryItemViewModel model)
        {
            var item = await CreateLibraryItem(model);
            await libraryItems.AddAsync(item);
        }

        public async Task UpdateItemAsync(CreateEditLibraryItemViewModel model)
        {
            var item = await CreateLibraryItem(model);
            await libraryItems.UpdateAsync(item);
        }

        public async Task BorrowItemAsync(BorrowItemViewModel model)
        {
            var item = await libraryItems.GetByIdAsync(model.Id);
            item.Borrower = model.Borrower;
            item.BorrowDate = DateTime.Now;

            await libraryItems.UpdateAsync(item);
        }

        public async Task ReturnItemAsync(int id)
        {
            var item = await libraryItems.GetByIdAsync(id);
            item.Borrower = null;
            item.BorrowDate = null;

            await libraryItems.UpdateAsync(item);
        }

        public async Task<List<LibraryItem>> GetAvailableItemsAsync()
        {
            var allLibraryItems = await libraryItems.GetAllAsync(x => x.Category);
            return allLibraryItems.ToList();
        }

        public async Task<LibraryItem> GetItemByIdAsync(int id)
        {
            return await libraryItems.GetByIdAsync(id);
        }

        public async Task<List<LibraryItem>> SearchItemsAsync(string query)
        {
            var allLibraryItems = await libraryItems.WhereAsync(x => x.Title == query || x.Author == query);
            return allLibraryItems.ToList();
        }

        public async Task<bool> ItemExistsAsync(int id)
        {
            return await libraryItems.AnyAsync(x => x.Id == id);
        }

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
