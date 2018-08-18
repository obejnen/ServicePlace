using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServicePlace.Website.Extensions
{
    public static class HtmlExtensions
    {
        public static IHtmlString Page(this HtmlHelper helper, string action, string controller, int currentPage,
            int minPage, int maxPage)
        {
            var html = new StringBuilder();
            html.AppendLine("<div class=\"w3-center w3-padding-32\">");
            html.AppendLine("<div class=\"w3-bar\">");
            var firstPage = currentPage == 1 || minPage == 1
                ? string.Empty
                : $"<a class=\"w3-bar-item w3-hover-black w3-button\" href=\"/{controller}/{action}/?page=1\">1</a>";
            var pageRange = Enumerable.Range(minPage, maxPage)
                .Select(x => x == currentPage
                    ? $"<a class=\"w3-bar-item w3-black w3-button\" href=\"/{controller}/{action}/?page={x}\">{x}</a>"
                    : $"<a class=\"w3-bar-item w3-hover-black w3-button\" href=\"/{controller}/{action}/?page={x}\">{x}</a>")
                .ToArray();
            var lastPage = currentPage == maxPage
                ? string.Empty
                : $"<a class=\"w3-bar-item w3-hover-black w3-button\" href=\"/{controller}/{action}/?page={maxPage}\">Last</a>";
            html.AppendLine(firstPage);
            html.AppendLine(string.Join("\n", pageRange));
            html.AppendLine(lastPage);
            html.AppendLine("</div></div>");
            return new HtmlString(html.ToString());
        }
    }
}