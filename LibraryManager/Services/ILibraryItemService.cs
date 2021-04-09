using LibraryManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Services
{
    public interface ILibraryItemService
    {
        Task AddItemAsync(LibraryItem item);
        Task DeleteItemAsync(int id);
        Task<List<LibraryItem>> GetAvailableItemsAsync();
        Task<LibraryItem> GetItemByIdAsync(int id);
        Task<bool> ItemExistsAsync(int id);
        Task<List<LibraryItem>> SearchItemsAsync(string query);
        Task UpdateItemAsync(LibraryItem item);
    }
}