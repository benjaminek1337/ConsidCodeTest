using LibraryManager.Models;
using LibraryManager.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Services
{
    public interface ILibraryItemService
    {
        Task AddItemAsync(CreateEditLibraryItemViewModel model);
        Task BorrowItemAsync(BorrowItemViewModel model);
        Task DeleteItemAsync(int id);
        Task<List<LibraryItem>> GetAvailableItemsAsync();
        Task<LibraryItem> GetItemByIdAsync(int id);
        Task<bool> ItemExistsAsync(int id);
        Task ReturnItemAsync(int id);
        Task<List<LibraryItem>> SearchItemsAsync(string query);
        Task UpdateItemAsync(CreateEditLibraryItemViewModel model);
    }
}