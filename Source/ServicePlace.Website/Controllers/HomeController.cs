using System.Web.Mvc;

namespace ServicePlace.Website.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Order");
        }
    }
}