using LibraryManager.Models;
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
        public LibraryItemService(ILibraryDbRepository<LibraryItem> libraryItems)
        {
            this.libraryItems = libraryItems;
        }

        public async Task DeleteItemAsync(int id)
        {
            var item = await GetItemByIdAsync(id);
            await libraryItems.DeleteAsync(item);
        }

        public async Task AddItemAsync(LibraryItem item)
        {
            await libraryItems.AddAsync(item);
        }

        public async Task UpdateItemAsync(LibraryItem item)
        {
            await libraryItems.UpdateAsync(item);
        }

        public async Task<List<LibraryItem>> GetAvailableItemsAsync()
        {
            var allLibraryItems = await libraryItems.WhereAsync(x => String.IsNullOrEmpty(x.Borrower), x => x.Category);
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

    }
}
