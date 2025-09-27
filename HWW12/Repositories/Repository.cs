using HWW12.DataAccess;
using HWW12.Entities;
using HWW12.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW12.Repositories
{
    public class Repository
    {
        AppDbContext _context = new AppDbContext();
        public List<User> GetUsers()
        {
            return _context.Users.ToList();

        } 
        public User? GetUserByUserName(string username)
        {
            return _context.Users.Include(u => u.BorrowedBooks)
                  .ThenInclude(bb => bb.Book)
                 .ThenInclude(b => b.Category)
                 .FirstOrDefault(u => u.UserName.ToLower() == username.ToLower());

        }
       
        public void AddUser(User user)
        {

            _context.Users.Add(user);
            _context.SaveChanges();

        }

        public List<Book> GetBooks() 
        {
              return _context.Books.Include(b=>b.Category).ToList();
        
        }

        public Book? GetBookById(int bookId) 
        {
            return _context.Books.Include(b=>b.Category).Include(b=>b.Reviews).SingleOrDefault(b => b.Id == bookId);
        
        }

        public List<Category> GetCategories() 
        {
           return _context.Categories.ToList();
        
        }

        public Category? GetCategoryByName(string name)
        {
            return _context.Categories.FirstOrDefault(c => c.Genre.ToLower() == name.ToLower());
        }

        public Category? GetCategoryById(int categoryId) 
        {
          return  _context.Categories.SingleOrDefault(c=>c.Id==categoryId);    
        }
        public void AddBorrowedBook(BorrowedBook borrowedBook) 
        {
            _context.BorrowedBooks.Add(borrowedBook);
            _context.SaveChanges();
        
        }
        public List<BorrowedBook> GetBorrowedBooksByUserId(int userId)
        {
          
            return _context.BorrowedBooks.Where(bb=>bb.User.Id==userId).Include(bb=>bb.Book)
                .ThenInclude(b=>b.Category).ToList();     
        }
        public BorrowedBook? FindBorrowedBookByUserId(int userId , int bookId) 
        {
           return _context.BorrowedBooks
                .Include(bb=>bb.Book)
                .FirstOrDefault(bb=>bb.UserId==userId && bb.BookId==bookId);      
        }
        public void AddCategory(Category category) 
        {
            _context.Categories.Add(category);
            _context.SaveChanges();       
        }
        public void AddBook(Book book) 
        {
           _context.Books.Add(book);
           _context.SaveChanges();
        }

        public void AddReview(Review review) 
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }
        public void UpdateReview(Review review) 
        {
           _context.Reviews.Update(review);
            _context.SaveChanges();
        }
        public Review? FindReviewByUserAndBook(int bookId , int userId) 
        {
           return _context.Reviews.FirstOrDefault(r=>r.BookId==bookId && r.UserId==userId);
        }

        public List<Review> GetMyReviewsByUserId(int userId) 
        {
           return _context.Reviews.Include(r=>r.Book).Where(r=>r.UserId==userId).ToList(); 
        }

        public Review? GetReviewByReviewId(int reviewId , int userId) 
        {
           return _context.Reviews.FirstOrDefault(r => r.UserId == userId && r.Id == reviewId);
        }

        public void RemoveReview(Review review) 
        {
           _context.Reviews.Remove(review);
            _context.SaveChanges();
        }

        public List<Review> GetUnverifiedReviews() 
        {
           return _context.Reviews
                .Include(r=>r.Book)
                .Include(r=>r.User)
                .Where(r=>r.ReviewStatus == 0)
                .ToList();
          
        }

        public Review? GetReviewByReviewId(int reviewId)
        {
            return _context.Reviews.FirstOrDefault(r => r.Id == reviewId);
        }

        
    }
}
