using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ServicePlace.Website.Controllers
{
    [Route("[controller]")]
    public class BaseController : Controller
    {

        protected IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}