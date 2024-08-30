using Checkpoint2.Areas.Identity.Data;
using Checkpoint2.Models.Entities;
using Checkpoint2.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WildPay.Exceptions;

namespace Checkpoint2.Controllers
{
    public class OrdersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICartRepository _cartRepository;
        private readonly IBooksRepository _booksRepository;
        private readonly IOrdersRepository _ordersRepository;

        public OrdersController(UserManager<ApplicationUser> userManager, ICartRepository cartRepository, IBooksRepository booksRepository, IOrdersRepository ordersRepository)
        {
            _userManager = userManager;
            _ordersRepository = ordersRepository;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (userId is null) return NotFound();

                List<Order> userOrders = await _ordersRepository.GetOrdersByUserIdAsync(userId);

                return View(userOrders);
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
