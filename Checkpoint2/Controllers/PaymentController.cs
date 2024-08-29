using Checkpoint2.Models.Entities;
using Checkpoint2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WildPay.Exceptions;
using Checkpoint2.Areas.Identity.Data;
using Checkpoint2.Repositories;

namespace Checkpoint2.Controllers
{
    public class PaymentController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICartRepository _cartRepository;
        private readonly IBooksRepository _booksRepository;
        private readonly IOrdersRepository _ordersRepository;

        public PaymentController(UserManager<ApplicationUser> userManager, ICartRepository cartRepository, IBooksRepository booksRepository, IOrdersRepository ordersRepository)
        {
            _userManager = userManager;
            _cartRepository = cartRepository;
            _booksRepository = booksRepository;
            _ordersRepository = ordersRepository;
        }

        public async Task<IActionResult> Payment()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (userId is null) return NotFound();

                List<CartArticle> cartArticles = await _cartRepository.GetCartArticlesByUserIdAsync(userId);

                ViewBag.Total = cartArticles.Sum(el => el.Quantity * el.Book.Price);

                return View();
            }
            catch (DatabaseException ex)
            {
                return RedirectToAction(actionName: "Exception", controllerName: "Home", new
                {
                    message = ex.Message
                });
            }
            catch (NullException ex)
            {
                return RedirectToAction(actionName: "Exception", controllerName: "Home", new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(CreditCardModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Payment", model);
                }

                var userId = _userManager.GetUserId(User);
                if (userId is null) return NotFound();

                await _cartRepository.TransferArticlesByUserId(userId);

                List<BuyedArticle> buyedArticles = await _ordersRepository.GetBuyedArticlesByUserIdAsync(userId);

                Order order = new Order
                {
                    ApplicationUserId = userId,
                    Articles = buyedArticles,
                    Date = DateTime.Now
                };

                await _ordersRepository.AddOrderAsync(order);


                return RedirectToAction(actionName: "SuccessCommand", controllerName: "Payment");
            }
            catch (DatabaseException ex)
            {
                return RedirectToAction(actionName: "Exception", controllerName: "Home", new
                {
                    message = ex.Message
                });
            }
            catch (NullException ex)
            {
                return RedirectToAction(actionName: "Exception", controllerName: "Home", new { message = ex.Message });
            }
        }

        public async Task<IActionResult> SuccessCommand()
        {
            return View();
        }

    }
}
