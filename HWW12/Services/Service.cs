using HWW12.DTO;
using HWW12.Entities;
using HWW12.Enums;
using HWW12.Exceptions;
using HWW12.Managers;
using HWW12.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HWW12.Services
{
    public class Service
    {
        Repository repository = new Repository();
        public void Register(string username, string password, RoleEnum role)
        {
            User userFromDb = repository.GetUserByUserName(username);
            if (userFromDb != null)
            {
                throw new UserAlreadyExistsException("Username already exists.");
            }
            User user = new User(username, password, role);
            repository.AddUser(user);

        }

        public void LogIn(string username, string password)
        {
            User userFromDb = repository.GetUserByUserName(username);
            if (userFromDb == null)
            {
                throw new LoginFailedException("The username or password is incorrect.");
            }
            if (userFromDb.CheckPass(password))
            {
                CurrentUserSession.SetCurrentUser(userFromDb);
            }
            if (!userFromDb.CheckPass(password))
            {
                throw new LoginFailedException("The username or password is incorrect.");
            }

        }

        public BooksAndCategoriesDTO ListBooksAndCategories()
        {
            List<Category> categories = repository.GetCategories();
            List<Book> books = repository.GetBooks();

            var booksAndCategories = new BooksAndCategoriesDTO
            {
                Books = books,
                Categories = categories
            };
            return booksAndCategories;
        }
        public void BorrowBook(int bookId)
        {
            Book book = repository.GetBookById(bookId);
            if (book == null)
            {
                throw new BookNotFoundException("No book with this ID was found.");
            }
            if (CurrentUserSession.LoggedInUser == null)
            {
                throw new UserNotLoggedInException("User is not logged in.");
            }
            var borrowedBook = new BorrowedBook
            {
                Book = book,
                User = CurrentUserSession.LoggedInUser,
                BorrowDate = DateTime.Now,
            };
            repository.AddBorrowedBook(borrowedBook);
        }

        public List<BorrowedBook> ListMyBorrowedBook()
        {
            if (CurrentUserSession.LoggedInUser == null)
            {
                throw new UserNotLoggedInException("User is not logged in.");
            }

            return repository.GetBorrowedBooksByUserId(CurrentUserSession.LoggedInUser.Id);

        }

        public void AddCategory(string genre)
        {

            if (CurrentUserSession.LoggedInUser == null)
            {
                throw new UserNotLoggedInException("User is not logged in.");
            }
            Category category = repository.GetCategoryByName(genre);

            if (category != null)
            {
                throw new DuplicateCategoryException("This category has already been defined.");
            }
            category = new Category
            {
                Genre = genre
            };
            repository.AddCategory(category);

        }

        public void AddBook(string title, int categoryId)
        {
            Category category = repository.GetCategoryById(categoryId);
            if (category == null)
            {
                throw new CategoryNotFoundException("No category found with this ID.");
            }
            Book book = new Book
            {
                Title = title,
                Category = category
            };
            repository.AddBook(book);
        }

        public List<Category> ShowListCategries()
        {
            return repository.GetCategories();
        }

        public void AddReview(int bookId, string comment, int rating)
        {
            Book book = repository.GetBookById(bookId);
            if (book == null)
            {
                throw new BookNotFoundException("No book with this ID was found.");
            }
            BorrowedBook borrowBook = repository.FindBorrowedBookByUserId(CurrentUserSession.LoggedInUser.Id, bookId);
            if (borrowBook == null)
            {
                throw new BorrowedBookNotFoundException("You can only comment on books you have borrowed.");
            }
            Review reviewFromDb = repository.FindReviewByUserAndBook(bookId, CurrentUserSession.LoggedInUser.Id);
            if (reviewFromDb != null)
            {
                throw new DuplicateReviewException("You have already commented on this book.");
            }
            if (rating > 5 || rating < 1)
            {
                throw new InvalidRatingException("Rating must be between 1 and 5.");
            }
            var review = new Review
            {
                Comment = comment,
                Rating = rating,
                CreatedAt = DateTime.Now,
                UserId = CurrentUserSession.LoggedInUser.Id,
                BookId = bookId,
                ReviewStatus = ReviewStatusEnum.Pending

            };
            repository.AddReview(review);
        }

        public List<Review> GetMyReviews()
        {
            if (CurrentUserSession.LoggedInUser == null)
            {
                throw new UserNotLoggedInException("User is not logged in.");
            }
            return repository.GetMyReviewsByUserId(CurrentUserSession.LoggedInUser.Id);
        }

        public void EditReview(int bookId, string comment, int rating)
        {
            if (CurrentUserSession.LoggedInUser == null)
            {
                throw new UserNotLoggedInException("User is not logged in.");
            }
            Review review = repository.FindReviewByUserAndBook(bookId, CurrentUserSession.LoggedInUser.Id);
            if (review == null)
            {
                throw new ReviewNotFoundException("You have not submitted a review for this book.");
            }
            if (rating > 5 || rating < 1)
            {
                throw new InvalidRatingException("Rating must be between 1 and 5.");
            }
            review.Comment = comment;
            review.Rating = rating;
            repository.UpdateReview(review);
        }

        public void RemoveReview(int reviewId)
        {

            if (CurrentUserSession.LoggedInUser == null)
            {
                throw new UserNotLoggedInException("User is not logged in.");
            }
            Review? review = repository.GetReviewByReviewId(reviewId, CurrentUserSession.LoggedInUser.Id);
            if (review == null)
            {
                throw new ReviewNotFoundException("You have not submitted a review for this book");
            }
            repository.RemoveReview(review);
        }

        public List<Review> GetUnverifiedReviews()
        {
            if (CurrentUserSession.LoggedInUser == null)
            {
                throw new UserNotLoggedInException("You are not logged in..");
            }
            return repository.GetUnverifiedReviews();
        }

        public void ApproveReview(int reviewId) 
        {
            Review review=repository.GetReviewByReviewId(reviewId);
            if (review==null)
            {
                throw new ReviewNotFoundException("A review with this ID is invalid.");
            }
            review.ReviewStatus = ReviewStatusEnum.Approved;
            repository.UpdateReview(review);

        }

        public void RejectReview(int reviewId) 
        {
            Review review = repository.GetReviewByReviewId(reviewId);
            if (review == null)
            {
                throw new ReviewNotFoundException("A review with this ID is invalid.");
            }
            review.ReviewStatus = ReviewStatusEnum.Rejected;
            repository.UpdateReview(review);
        }

        public BookDetailsDTO GetBookDetails(int bookId) 
        {
            Book? book= repository.GetBookById(bookId);
            if (book==null)
            {
                throw new BookNotFoundException("There is no book with this ID.");
            }
            if (CurrentUserSession.LoggedInUser == null)
            {
                throw new UserNotLoggedInException("You are not logged in..");
            }
            List<Review> reviewsIsApproved = book.Reviews.Where(r => r.ReviewStatus== ReviewStatusEnum.Approved).ToList();
            BookDetailsDTO bookDetailsDTO = new BookDetailsDTO();
            bookDetailsDTO.Reviews = reviewsIsApproved;
            if (reviewsIsApproved.Count==0)
            {
                
                bookDetailsDTO.AvgRating = 0;              
            }
            else 
            {

              

               bookDetailsDTO.AvgRating = reviewsIsApproved.Average(r => r.Rating);
                
            }
            
            return bookDetailsDTO;
        }
    }
}
