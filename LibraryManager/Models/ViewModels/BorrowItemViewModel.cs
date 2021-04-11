using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Models.ViewModels
{
    public class BorrowItemViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "A name is required")]
        public string Borrower { get; set; }
    }
}
