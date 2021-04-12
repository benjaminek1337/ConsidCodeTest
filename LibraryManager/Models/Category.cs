using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Category name is required.")]
        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        public List<LibraryItem> LibraryItems { get; set; }
    }
}
