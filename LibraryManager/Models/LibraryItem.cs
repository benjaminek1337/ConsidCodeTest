using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Models
{
    public class LibraryItem
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A category is required")]
        [ForeignKey("Category")]
        [Display(Name = "Category")]

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required(ErrorMessage = "A title is required")]  
        public string Title { get; set; }

        public string Author { get; set; }

        public int? Pages { get; set; }

        [Display(Name = "Run time (minutes)")]
        public int? RunTimeMinutes { get; set; }

        [Required(ErrorMessage = "The borrowability needs to be set")]
        //[Column(TypeName = "bit")]
        public bool IsBorrowable { get; set; }

        public string Borrower { get; set; }

        [Display(Name = "Date when borrowed")]
        public DateTime? BorrowDate { get; set; }

        [Required(ErrorMessage = "A type of item is required")]
        public string Type { get; set; }

    }
}
