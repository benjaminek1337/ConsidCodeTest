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

        public async Task DeleteLibraryItem(LibraryItem item)
        {
            await libraryItems.Delete(item);
        }

        public async Task AddItem(LibraryItem item)
        {
            await libraryItems.Add(item);
        }

        public async Task UpdateItem(LibraryItem item)
        {
            await libraryItems.Update(item);
        }

        public async Task<List<LibraryItem>> GetAvailableLibraryItems()
        {
            var allLibraryItems = await libraryItems.Select(x => String.IsNullOrEmpty(x.Borrower));
            return allLibraryItems.ToList();
        }

        public async Task<List<LibraryItem>> SearchItems(string query)
        {
            var allLibraryItems = await libraryItems.Select(x => x.Title == query || x.Author == query);
            return allLibraryItems.ToList();
        }

    }
}
