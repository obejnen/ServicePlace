using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ServicePlace.Website.Filters
{
    public class CultureFilter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var cultureCookie = filterContext.HttpContext.Request.Cookies["lang"];
            var cultureName = cultureCookie != null ? cultureCookie.Value : "en";

            var cultures = new List<string> {"en", "ru"};
            if (!cultures.Contains(cultureName))
                cultureName = "en";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }
}