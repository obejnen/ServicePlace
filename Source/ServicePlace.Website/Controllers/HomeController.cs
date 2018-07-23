using Microsoft.AspNetCore.Mvc;
using ServicePlace.Logic;

namespace ServicePlace.Website.Controllers
{
    public class HomeController : Controller
    {
        private OrderRepository repository = OrderSeeder.GetRepository(10);
        public ViewResult Index()
        {
            return View(repository);
        }
    }
}