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

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required(ErrorMessage = "A category id is required")]
        [Column(TypeName = "nvarchar(255)")]
        public string Title { get; set; }

        [Required(ErrorMessage = "A title is required")]
        [Column(TypeName = "nvarchar(255)")]
        public string Author { get; set; }

        public int? Pages { get; set; }

        public int? RunTimeMinutes { get; set; }

        [Required(ErrorMessage = "The borrowability needs to be set")]
        public bool IsBorrowable { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Borrower { get; set; }

        public DateTime? BorrowDate { get; set; }

        [Required(ErrorMessage = "A type of item is required")]
        [Column(TypeName = "nvarchar(255)")]
        public string Type { get; set; }

    }
}
