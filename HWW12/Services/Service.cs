using HWW12.DTO;
using HWW12.Entities;
using HWW12.Enums;
using HWW12.Exceptions;
using HWW12.Managers;
using HWW12.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if(!userFromDb.CheckPass(password))
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
            Category category=repository.GetCategoryById(categoryId);
            if (category==null)
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
    }
}
