using HWW12.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW12.DataAccess
{
    public class AppDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<BorrowedBook> BorrowedBooks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {           
            var connectionString = "Server=ALI\\SQLEXPRESS;Database=MyLibrary;Integrated Security=True;TrustServerCertificate=True;";          
            optionsBuilder.UseSqlServer(connectionString);
        }

        
    }
}
