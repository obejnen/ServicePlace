using Microsoft.AspNetCore.Mvc;
using ServicePlace.Logic;

namespace ServicePlace.Website.Controllers
{
    public class HomeController : Controller
    {
        private OrderService service = OrderInitializer.GetService(10);
        public ViewResult Index()
        {
            return View(service);
        }
    }
}