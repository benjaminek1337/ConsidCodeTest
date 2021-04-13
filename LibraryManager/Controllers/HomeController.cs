using LibraryManager.Models;
using LibraryManager.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILibraryItemService libraryItemService;
        private readonly ICategoryService categoryService;

        public HomeController(ILogger<HomeController> logger, ILibraryItemService libraryItemService, ICategoryService categoryService)
        {
            _logger = logger;
            this.libraryItemService = libraryItemService;
            this.categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            //var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            return View(new ErrorViewModel 
                { 
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    ErrorCode = Response.StatusCode

                });
        }
    }
}
