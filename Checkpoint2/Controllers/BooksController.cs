using Checkpoint2.Models.Entities;
using Checkpoint2.Repositories;
using Microsoft.AspNetCore.Mvc;
using WildPay.Exceptions;

namespace Checkpoint2.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksRepository _booksRepository;

        public BooksController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                List<Book> books = await _booksRepository.GetAllBooksAsync();
                return View(books);
            }
            catch (DatabaseException ex)
            {
                return RedirectToAction(actionName: "Exception", controllerName: "Home", new
                {
                    message = ex.Message
                });
            }
        }
    }
}
