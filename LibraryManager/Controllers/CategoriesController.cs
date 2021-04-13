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
using Microsoft.AspNetCore.Http;

namespace LibraryManager.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var items = await categoryService.GetCategoriesAsync();
            items = items.OrderBy(x => x.CategoryName).ToList();
            return View(items);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var category = await categoryService.GetCategoryByIdAsync(id);
            if(id == 0 && category == null)
            {
                return BadRequest();
            }
            else if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create(bool creationFailed)
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                if (await categoryService.AddCategoryAsync(category))
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["CreationError"] = $"The category {category.CategoryName} already exists";
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = await categoryService.GetCategoryByIdAsync(id);
            if (id == 0 && category == null)
            {
                return BadRequest();
            }
            else if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(await categoryService.UpdateCategoryAsync(category))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await categoryService.CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }      
            }

            ViewData["CreationError"] = $"The category {category.CategoryName} already exists";
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int id, bool deleteFailed)
        {
            var category = await categoryService.GetCategoryByIdAsync(id);

            if (id == 0 && category == null)
            {
                return BadRequest();
            }
            else if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await categoryService.GetCategoryByIdAsync(id);
            if (await categoryService.DeleteCategoryAsync(category))
            {
                return RedirectToAction(nameof(Index));
            }

            ViewData["DeleteError"] = "The category cannot be deleted because it contains some library items";
            return View(category);
        }
    }
}
