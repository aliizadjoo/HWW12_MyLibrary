
using HWW12.DTO;
using HWW12.Entities;
using HWW12.Enums;
using HWW12.Exceptions;
using HWW12.Managers;
using HWW12.Services;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
Service service = new Service();

while (true)
{
    if (CurrentUserSession.LoggedInUser == null)
    {

        try
        {
            Console.WriteLine("1.Rgister or 2.LogIn");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Please enter username");
                    string username = Console.ReadLine();
                    Console.WriteLine("please enter password");
                    string password = Console.ReadLine();
                    Console.WriteLine("please enter Number role : User , Admin");
                    string choiceRole = Console.ReadLine();
                    try
                    {
                        RoleEnum roleEnum = (RoleEnum)Enum.Parse(typeof(RoleEnum), choiceRole);
                        service.Register(username, password, roleEnum);
                        Console.WriteLine("Registration was successful.");
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Invalid role please enter , User or Admin :");
                    }
                    catch (UserAlreadyExistsException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    break;
                case 2:
                    Console.WriteLine("please enter username");
                    username = Console.ReadLine();
                    Console.WriteLine("please enter password");
                    password = Console.ReadLine();
                    try
                    {
                        service.LogIn(username, password);
                        Console.WriteLine("LogIn  successful.");


                    }
                    catch (LoginFailedException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    break;
                default:
                    Console.WriteLine("invalid Option,please enter number :1.Rgister or 2.LogIn");
                    break;
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("please enter number ");
        }

    }

    else
    {
        switch (CurrentUserSession.LoggedInUser.Role)
        {
            case RoleEnum.User:
                ShowMenuUser();
                try
                {
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            ListBooksAndCategories();
                            break;
                        case 2:
                            Console.WriteLine("please enter BookId");
                            ListBooksAndCategories();
                            try
                            {
                                int choiceBook = int.Parse(Console.ReadLine());
                                service.BorrowBook(choiceBook);
                                Console.WriteLine("The book was successfully borrowed.");
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("invalid BookId");

                            }
                            catch (UserNotLoggedInException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (BookNotFoundException e)
                            {
                                Console.WriteLine(e.Message);
                            }

                            break;
                        case 3:
                            try
                            {
                                ListMyBorrowedBook();
                            }
                            catch (UserNotLoggedInException e)
                            {
                                Console.WriteLine(e.Message);
                            }

                            break;
                        case 4:
                            ListBooksAndCategories();
                            Console.WriteLine("Select the book ID you want to comment on from the list.");
                            try
                            {
                                int bookId = int.Parse(Console.ReadLine());
                                Console.WriteLine("please enter comment");
                                string comment = Console.ReadLine();
                                Console.WriteLine("please enter rating");
                                int rating = int.Parse(Console.ReadLine());
                                service.AddReview(bookId, comment, rating);
                                Console.WriteLine("add review is done");
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("invalid bookId  Select the book ID you want to comment on from the list");
                            }
                            catch (BookNotFoundException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (BorrowedBookNotFoundException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (DuplicateReviewException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (InvalidRatingException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        case 5:
                            Console.WriteLine("1.Edit or 2.Remove");

                            try
                            {
                                int option = int.Parse(Console.ReadLine());
                                Console.WriteLine("--------------------");
                               
                                switch (option)
                                {
                                    case 1:
                                        try
                                        {
                                            Console.WriteLine("Select the desired bookId.");
                                            ShowListMyReview();
                                            int bookId = int.Parse(Console.ReadLine());
                                            Console.WriteLine("please enter comment");
                                            string commentChanged = Console.ReadLine();
                                            int ratingChanged = int.Parse(Console.ReadLine());
                                            service.EditReview(bookId, commentChanged, ratingChanged);
                                            Console.WriteLine("remove is done");

                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("invalid bookId Select the book ID from the submission list.");
                                        }
                                        catch(UserNotLoggedInException e) 
                                        {
                                            Console.WriteLine(e.Message);
                                        }
                                        catch(ReviewNotFoundException e) 
                                        {
                                            Console.WriteLine(e.Message);
                                        }
                                        catch(InvalidRatingException e) 
                                        {
                                            Console.WriteLine(e.Message);
                                        }
                                        break;
                                    case 2:
                                        try 
                                        {

                                            Console.WriteLine("Select the desired ReviewId.");
                                            ShowListMyReview();
                                            int reviewId = int.Parse(Console.ReadLine());
                                            service.RemoveReview(reviewId);
                                            Console.WriteLine("remove is done");
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("invalid reviewId Select the review ID from the submission list.");
                                        }
                                        catch (UserNotLoggedInException e)
                                        {
                                            Console.WriteLine(e.Message);
                                        }
                                        catch (ReviewNotFoundException e)
                                        {
                                            Console.WriteLine(e.Message);
                                        }
                                        catch (InvalidRatingException e)
                                        {
                                            Console.WriteLine(e.Message);
                                        }
                                        break;
                                    default:
                                        Console.WriteLine("invalid option please select 1.Edit or 2.Remove ");
                                        break;
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("invalid option please select 1.Edit or 2.Remove ");
                            }
                            

                            break;
                        case 6:
                            CurrentUserSession.Logout();
                            break;
                        default:
                            Console.WriteLine("Invalid option Please choose a number from the list I sent.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid option in menuUser.");
                }
                break;

            case RoleEnum.Admin:

                ShowMenuAdmin();
                try
                {
                    int choiceOption = int.Parse(Console.ReadLine());
                    switch (choiceOption)
                    {
                        case 1:
                            Console.WriteLine("please enter Genre");
                            string genre = Console.ReadLine();

                            try
                            {
                                service.AddCategory(genre);
                                Console.WriteLine("Category added successfully.");
                            }
                            catch (UserNotLoggedInException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (DuplicateCategoryException e)
                            {
                                Console.WriteLine(e.Message);
                            }

                            break;
                        case 2:
                            Console.WriteLine("please enter title");
                            string title = Console.ReadLine();
                            Console.WriteLine("Please select a category:");
                            ShowListCategories();
                            try
                            {
                                int choiceCategori = int.Parse(Console.ReadLine());
                                service.AddBook(title, choiceCategori);
                                Console.WriteLine("Book added successfully.");

                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("invalid category");
                            }
                            catch (CategoryNotFoundException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        case 3:
                            ListBooksAndCategories();
                            break;
                        case 4:
                            CurrentUserSession.Logout();
                            break;
                        default:
                            Console.WriteLine("invalid option in menuAdmin");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Option in menuAdmin , please enter number");
                }
                break;
        }
    }
}
void ShowMenuUser()
{
    Console.WriteLine("please enter number:");
    Console.WriteLine("1.View Books and Categories");
    Console.WriteLine("2.Borrow a Book");
    Console.WriteLine("3.View My Borrowed Books");
    Console.WriteLine("4.Add Review");
    Console.WriteLine("5.Edit&Remove Review");
    Console.WriteLine("6.LogOut");
}

void ShowMenuAdmin()
{
    Console.WriteLine("please enter number:");
    Console.WriteLine("1.Add New Category");
    Console.WriteLine("2.Add New Book");
    Console.WriteLine("3.View All Books and Categories");
    Console.WriteLine("4.LogOut");
}

void ListBooksAndCategories()
{
    BooksAndCategoriesDTO booksAndCategories = service.ListBooksAndCategories();

    foreach (var category in booksAndCategories.Categories)
    {

        Console.WriteLine($"id:{category.Id}.{category}");

    }

    foreach (var book in booksAndCategories.Books)
    {
        Console.WriteLine(book);
    }
}

void ListMyBorrowedBook()
{
    List<BorrowedBook> borrowedBooks = service.ListMyBorrowedBook();
    foreach (var borrowedBook in borrowedBooks)
    {
        Console.WriteLine($"Book:{borrowedBook.Book.Title},Borrowed on:{borrowedBook.BorrowDate}");
    }

}

void ShowListCategories()
{
    List<Category> categories = service.ShowListCategries();

    foreach (var category in categories)
    {
        Console.WriteLine($"id:{category.Id}.{category}");
    }

}

void ShowListMyReview()
{
    List<Review> reviews = service.GetMyReviews();
    foreach (var review in reviews)
    {
        Console.WriteLine($"reviewId:{review.Id},BookId:{review.BookId} , title:{review.Book.Title} " +
            $", comment:{review.Comment} , rating:{review.Rating}");
    }

}


