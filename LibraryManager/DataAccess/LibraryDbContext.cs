using LibraryManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.DataAccess
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) {}

        public DbSet<Category> Categories { get; set; }
        public DbSet<LibraryItem> LibraryItems { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, CategoryName = "Fantasy" },
                new Category { Id = 2, CategoryName = "Sci-fi" },
                new Category { Id = 3, CategoryName = "Informatics" }
            };

            modelBuilder.Entity<Category>().HasData(categories);

            // Seeda mera sedan
            var libItems = new List<LibraryItem>
            {
                new LibraryItem { Id = 1, CategoryId = 1, Author = "JRR Tolkien", Title = "Sagan om ringen", Type = "Book", IsBorrowable = true, Pages = 400},
                new LibraryItem { Id = 2, CategoryId = 2, Title = "Star Wars - A New Hope", Type = "DVD", IsBorrowable = true, RunTimeMinutes = 120},
                new LibraryItem { Id = 3, CategoryId = 3, Author = "Gulliksen, Göransson", Title = "Användarcentrerad Systemutveckling", Type = "Reference Litterature", IsBorrowable = false, Pages = 200}
            };

            modelBuilder.Entity<LibraryItem>().HasData(libItems);
        }
    }
}
