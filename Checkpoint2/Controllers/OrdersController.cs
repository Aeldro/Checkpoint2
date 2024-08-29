using Checkpoint2.Areas.Identity.Data;
using Checkpoint2.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            return View();
        }
    }
}
