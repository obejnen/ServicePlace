using Microsoft.AspNetCore.Mvc;

namespace ServicePlace.Website.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
    }
}