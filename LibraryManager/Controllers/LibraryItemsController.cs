using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManager.DataAccess;
using LibraryManager.Models;
using LibraryManager.Services;
using LibraryManager.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace LibraryManager.Controllers
{
    public class LibraryItemsController : Controller
    {
        private readonly ILibraryItemService libraryItemService;
        private readonly ICategoryService categoryService;

        public LibraryItemsController(ILibraryItemService libraryItemService, ICategoryService categoryService)
        {
            this.libraryItemService = libraryItemService;
            this.categoryService = categoryService;
        }

        // GET: LibraryItems
        public async Task<IActionResult> Index(string sortOrder)
        {
            var items = await libraryItemService.GetAvailableItemsAsync();

            sortOrder = !String.IsNullOrEmpty(sortOrder) ? sortOrder : HttpContext.Session.GetString("SortOrder");

            ViewData["SortByCategory"] = (String.IsNullOrEmpty(sortOrder) || sortOrder == "categoryAscending") ? "categoryDescending" : "categoryAscending";
            ViewData["SortByType"] = sortOrder == "typeAscending" ? "typeDescending" : "typeAscending";

            if (!String.IsNullOrEmpty(sortOrder))
            {
                HttpContext.Session.SetString("SortOrder", sortOrder);
            }

            switch (sortOrder)
            {
                case "categoryDescending":
                    items = items.OrderByDescending(x => x.Category.CategoryName).ToList();
                    break;
                case "typeAscending":
                    items = items.OrderBy(x => x.Type).ToList();
                    break;
                case "typeDescending":
                    items = items.OrderByDescending(x => x.Type).ToList();
                    break;
                default:
                    items = items.OrderBy(x => x.Category.CategoryName).ToList();
                    break;
            }

            return View(items);
        }

        // GET: LibraryItems/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var libraryItem = await libraryItemService.GetItemByIdAsync(id);
            if (libraryItem == null)
            {
                return NotFound();
            }

            return View(libraryItem);
        }

        // GET: LibraryItems/Create
        public async Task<IActionResult> Create(string type)
        {
            var model = new CreateEditLibraryItemViewModel();
            model.Type = String.IsNullOrWhiteSpace(type) ? model.Types[0] : type;
            model.Categories = await categoryService.GetCategoriesAsync();
            return View(model);
        }

        // POST: LibraryItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditLibraryItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                await libraryItemService.AddItemAsync(model);
                return RedirectToAction(nameof(Index));
            }

            model.Type = String.IsNullOrWhiteSpace(model.Type) ? model.Types[0] : model.Type;
            model.Categories = await categoryService.GetCategoriesAsync();
            return View(model);
        }

        // GET: LibraryItems/Edit/5
        public async Task<IActionResult> Edit(int id, string type)
        {
            var item = await libraryItemService.GetItemByIdAsync(id);
            var model = new CreateEditLibraryItemViewModel
            {
                Id = item.Id,
                Type = String.IsNullOrWhiteSpace(type) ? item.Type : type,
                Categories = await categoryService.GetCategoriesAsync(),
                Title = item.Title,
                Author = item.Author,
                Pages = item.Pages,
                RunTimeMinutes = item.RunTimeMinutes,
                CategoryId = item.CategoryId
            };
            return View(model);
        }

        // POST: LibraryItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateEditLibraryItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await libraryItemService.UpdateItemAsync(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await libraryItemService.ItemExistsAsync(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: LibraryItems/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var libraryItem = await libraryItemService.GetItemByIdAsync(id);
            return View(libraryItem);
        }

        // POST: LibraryItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await libraryItemService.DeleteItemAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: LibraryItems/Borrow/3
        public IActionResult Borrow(int id)
        {
            var model = new BorrowItemViewModel
            {
                Id = id
            };

            return View(model);
        }

        // POST: LibraryItems/Borrow/3
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Borrow(int id, BorrowItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await libraryItemService.BorrowItemAsync(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await libraryItemService.ItemExistsAsync(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET LibraryItems/Return/3
        public async Task<IActionResult> Return(int id)
        {
            await libraryItemService.ReturnItemAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
