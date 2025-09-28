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
        public DbSet<Review> Reviews { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }

        public DbSet<Wishlist> Wishlist { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=ALI\\SQLEXPRESS;Database=MyLibraryCww12parta;Integrated Security=True;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.UserName).HasMaxLength(128).IsRequired();
                entity.Property<string>("Password").IsRequired();

            });
            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Reviews");
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Comment).HasMaxLength(300);
                entity.Property(r => r.Rating).IsRequired();
                entity.HasOne(r => r.User)
                      .WithMany(u => u.Reviews)
                      .HasForeignKey(r => r.UserId);

                entity.HasOne(r => r.Book)
                .WithMany(b => b.Reviews)
                .HasForeignKey(r => r.BookId);
            });

            modelBuilder.Entity<Book>(entity =>
            {

                entity.ToTable("Books");
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Title).HasMaxLength(200).IsRequired();           
                entity.HasOne(b => b.Category)
                .WithMany(c => c.Books).HasForeignKey(b=>b.CategoryId);
                

            });

            modelBuilder.Entity<Category>(entity =>
            {

                entity.ToTable("Categories");
                entity.HasKey(c => c.Id);
                entity.Property(c=>c.Genre).HasMaxLength(100).IsRequired(); 
              


            });

            modelBuilder.Entity<BorrowedBook>(entity =>
            {

                entity.ToTable("BorrowedBooks");
                entity.HasKey(bb =>bb.Id);

                entity.HasOne(bb => bb.Book)
                .WithMany(b => b.BorrowedBooks)
                .HasForeignKey(bb => bb.BookId);

                entity.HasOne(bb=>bb.User)
                .WithMany(u=>u.BorrowedBooks)
                .HasForeignKey(bb=>bb.UserId);




            });

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.HasKey(w=>w.Id);
                entity.ToTable("Wishlist");
                entity.HasOne(w => w.Book)
                .WithMany(b=>b.Wishlist).HasForeignKey(w=>w.BookId).IsRequired();
               
                entity.HasOne(w => w.User)
                .WithMany(u => u.Wishlist).HasForeignKey(w => w.UserId).IsRequired();

            });
            




        }

    }
}
