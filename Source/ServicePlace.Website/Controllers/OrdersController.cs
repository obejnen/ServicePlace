using Microsoft.AspNetCore.Mvc;
using ServicePlace.Logic;

namespace ServicePlace.Website.Controllers
{
    public class OrdersController : Controller
    {
        private OrderRepository repository = OrderSeeder.GetRepository(10);
        public IActionResult Index()
        {
            return View(repository);
        }
    }
}