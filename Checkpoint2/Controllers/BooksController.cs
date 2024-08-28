using Checkpoint2.Models.Entities;
using Checkpoint2.Repositories;
using Microsoft.AspNetCore.Mvc;
using WildPay.Exceptions;
using static System.Reflection.Metadata.BlobBuilder;

namespace Checkpoint2.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksRepository _booksRepository;

        public BooksController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<IActionResult> Index(string searchedString)
        {
            try
            {
                if (searchedString == null || searchedString == string.Empty)
                {
                    List<Book> books = await _booksRepository.GetAllBooksAsync();
                    return View(books);
                }
                else
                {
                    List<Book> books = await _booksRepository.GetBooksAsync(searchedString);
                    return View(books);
                }
            }
            catch (DatabaseException ex)
            {
                return RedirectToAction(actionName: "Exception", controllerName: "Home", new
                {
                    message = ex.Message
                });
            }
        }

        public async Task<IActionResult> SearchResults(string searchedString)
        {
            try
            {
                List<Book> books;

                if (string.IsNullOrEmpty(searchedString))
                {
                    books = await _booksRepository.GetAllBooksAsync();
                }
                else
                {
                    books = await _booksRepository.GetBooksAsync(searchedString);
                }

                return PartialView("_SearchResults", books);
            }
            catch (DatabaseException ex)
            {
                return RedirectToAction(actionName: "Exception", controllerName: "Home", new
                {
                    message = ex.Message
                });
            }
        }

        public async Task<IActionResult> Details(int bookId)
        {
            try
            {
                Book book = await _booksRepository.GetBookByIdAsync(bookId);
                return View(book);
            }
            catch (DatabaseException ex)
            {
                return RedirectToAction(actionName: "Exception", controllerName: "Home", new { message = ex.Message });
            }
            catch (NullException ex)
            {
                return RedirectToAction(actionName: "Exception", controllerName: "Home", new { message = ex.Message });
            }
        }
    }
}
