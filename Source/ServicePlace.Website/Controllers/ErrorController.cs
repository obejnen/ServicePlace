using System.Web.Mvc;

namespace ServicePlace.Website.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult Error500()
        {
            return View();
        }
    }
}