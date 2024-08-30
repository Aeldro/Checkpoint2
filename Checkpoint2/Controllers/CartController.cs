using Checkpoint2.Areas.Identity.Data;
using Checkpoint2.Models;
using Checkpoint2.Models.Entities;
using Checkpoint2.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WildPay.Exceptions;
using static System.Reflection.Metadata.BlobBuilder;

namespace Checkpoint2.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICartRepository _cartRepository;
        private readonly IBooksRepository _booksRepository;

        public CartController(UserManager<ApplicationUser> userManager, ICartRepository cartRepository, IBooksRepository booksRepository)
        {
            _userManager = userManager;
            _cartRepository = cartRepository;
            _booksRepository = booksRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int bookId)
        {
            try
            {
                Book bookArticleToAdd = await _booksRepository.GetBookByIdAsync(bookId);
                var userId = _userManager.GetUserId(User);
                if (userId is null) return NotFound();

                CartArticle newArticle = new CartArticle
                {
                    BookId = bookId,
                    ApplicationUserId = userId,
                    Quantity = 1,
                    IsOrdered = false
                };

                await _cartRepository.AddArticleAsync(newArticle);

                return RedirectToAction("Index", "Books");
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

        public async Task<IActionResult> ViewCart()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (userId is null) return NotFound();

                List<CartArticle> cartArticles = await _cartRepository.GetCartArticlesByUserIdAsync(userId);

                return View(cartArticles);
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

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int articleId)
        {
            try
            {
                CartArticle articleToDelete = await _cartRepository.GetArticleByIdAsync(articleId);
                await _cartRepository.RemoveArticleAsync(articleToDelete);

                return RedirectToAction("ViewCart", "Cart");
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

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int articleId, int quantity)
        {
            try
            {
                if (quantity > 0)
                {
                    await _cartRepository.UpdateQuantityArticleAsync(articleId, quantity);

                    var userId = _userManager.GetUserId(User);
                    if (userId is null) return NotFound();

                    //List<CartArticle> cartArticles = await _cartRepository.GetCartArticlesByUserIdAsync(userId);

                    return Json(new { success = true });
                }
                else
                {
                    CartArticle cartArticle = await _cartRepository.GetArticleByIdAsync(articleId);
                    await _cartRepository.RemoveArticleAsync(cartArticle);

                    return Json(new { success = true });
                }
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

        public async Task<IActionResult> Checkout()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (userId is null) return NotFound();

                List<CartArticle> cartArticles = await _cartRepository.GetCartArticlesByUserIdAsync(userId);

                return View(cartArticles);
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

       
    }

}
