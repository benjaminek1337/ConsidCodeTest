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
        public async Task<IActionResult> Index()
        {
            return View(await libraryItemService.GetAvailableItemsAsync());
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
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await categoryService.GetCategoriesAsync(), "Id", "CategoryName");
            return View();
        }

        // POST: LibraryItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryId,Title,Author,Pages,RunTimeMinutes,IsBorrowable,Borrower,BorrowDate,Type")] LibraryItem libraryItem)
        {
            if (ModelState.IsValid)
            {
                await libraryItemService.AddItemAsync(libraryItem);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await categoryService.GetCategoriesAsync(), "Id", "CategoryName", libraryItem.CategoryId);
            return View(libraryItem);
        }

        // GET: LibraryItems/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var libraryItem = await libraryItemService.GetItemByIdAsync(id);
            if (libraryItem == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await categoryService.GetCategoriesAsync(), "Id", "CategoryName", libraryItem.CategoryId);
            return View(libraryItem);
        }

        // POST: LibraryItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,Title,Author,Pages,RunTimeMinutes,IsBorrowable,Borrower,BorrowDate,Type")] LibraryItem libraryItem)
        {
            if (id != libraryItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await libraryItemService.UpdateItemAsync(libraryItem);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await libraryItemService.ItemExistsAsync(libraryItem.Id))
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
            ViewData["CategoryId"] = new SelectList(await categoryService.GetCategoriesAsync(), "Id", "CategoryName", libraryItem.CategoryId);
            return View(libraryItem);
        }

        // GET: LibraryItems/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var libraryItem = await libraryItemService.GetItemByIdAsync(id);
            if (libraryItem == null)
            {
                return NotFound();
            }

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
    }
}
