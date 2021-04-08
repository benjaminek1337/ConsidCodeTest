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
        
        [Column(TypeName = "nvarchar(255)")]
        [Required(ErrorMessage = "Category name is required.")]
        public string CategoryName { get; set; }

        public List<LibraryItem> LibraryItems { get; set; }
    }
}
