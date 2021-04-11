using LibraryManager.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Models.ViewModels
{
    public class CreateEditLibraryItemViewModel
    {
        [Required(ErrorMessage = "A title is required")]
        public string Title { get; set; }

        public string Author { get; set; }

        public int? Pages { get; set; }

        [Display(Name = "Run time (minutes)")]
        public int? RunTimeMinutes { get; set; }

        [Required(ErrorMessage = "A type of item is required")]
        public string Type { get; set; }

        public List<string> Types { get; } = new List<string>
        {
            "Book",
            "DVD",
            "Audio Book",
            "Reference Book"
        };

        public List<Category> Categories { get; set; }

        [Required(ErrorMessage = "A category is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}
