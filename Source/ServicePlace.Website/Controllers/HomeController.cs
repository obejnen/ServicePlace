using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace ServicePlace.Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Order");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeLang(string lang)
        {
            var returnUrl = Request.UrlReferrer?.AbsolutePath;
            var cultures = new List<string>() { "ru", "en" };
            if (!cultures.Contains(lang))
            {
                lang = "en";
            }
            var cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;
            else
            {

                cookie = new HttpCookie("lang")
                {
                    HttpOnly = false,
                    Value = lang,
                    Expires = DateTime.Now.AddYears(1)
                };
            }
            Response.Cookies.Add(cookie);
            return Redirect(returnUrl);
        }
    }
}