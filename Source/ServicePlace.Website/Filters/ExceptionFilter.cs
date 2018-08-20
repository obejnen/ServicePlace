using System.Web.Mvc;

namespace ServicePlace.Website.Filters
{
    public class ExceptionFilterAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Error/Error500");
            filterContext.ExceptionHandled = true;
        }
    }
}