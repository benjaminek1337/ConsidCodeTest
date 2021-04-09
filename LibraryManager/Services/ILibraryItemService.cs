using LibraryManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Services
{
    public interface ILibraryItemService
    {
        Task AddItemAsync(LibraryItem item);
        Task DeleteItemAsync(LibraryItem item);
        Task<List<LibraryItem>> GetAvailableItemsAsync();
        Task<List<LibraryItem>> SearchItemsAsync(string query);
        Task UpdateItemAsync(LibraryItem item);
    }
}