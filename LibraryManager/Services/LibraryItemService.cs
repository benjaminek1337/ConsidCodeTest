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

        public async Task DeleteItemAsync(LibraryItem item)
        {
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
            var allLibraryItems = await libraryItems.SelectAsync(x => String.IsNullOrEmpty(x.Borrower));
            return allLibraryItems.ToList();
        }

        public async Task<List<LibraryItem>> SearchItemsAsync(string query)
        {
            var allLibraryItems = await libraryItems.SelectAsync(x => x.Title == query || x.Author == query);
            return allLibraryItems.ToList();
        }

    }
}
