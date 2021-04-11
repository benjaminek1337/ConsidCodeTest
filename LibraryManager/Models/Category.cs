using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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
