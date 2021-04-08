using LibraryManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.Services
{
    public interface ILibraryItemService
    {
        Task DeleteLibraryItem(LibraryItem item);
        Task<List<LibraryItem>> GetAvailableLibraryItems();
        Task UpdateItem(LibraryItem item);
    }
}