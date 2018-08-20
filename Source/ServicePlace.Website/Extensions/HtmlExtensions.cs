using System.Web;
using System.Web.Mvc;
using System.Text;

namespace ServicePlace.Website.Extensions
{
    public static class HtmlExtensions
    {
        public static IHtmlString Page(this HtmlHelper helper, string action, string controller, params int[] args)
        {
            var currentPage = args[0];
            var minPage = args[1];
            var maxPage = args[2];
            var id = args.Length == 4 ? (int?) args[3] : null;
            var html = new StringBuilder();
            html.AppendLine("<div class=\"w3-center w3-padding-32\">");
            html.AppendLine("<div class=\"w3-bar\">");
            var firstPage = currentPage == 1 || minPage == 1
                ? string.Empty
                : $"<a class=\"w3-bar-item w3-hover-black w3-button\" href=\"/{controller}/{action}/{id}?page=1\">1</a>";
            var pageRange = string.Empty;
            for (var i = minPage; i <= maxPage; i++)
            {
                pageRange += $"<a class=\"w3-bar-item w3{(i == currentPage ? "-" : "-hover-")}black w3-button\" " +
                             $"href=\"/{controller}/{action}/{id}?page={i}\">{i}</a>\n";
            }
            var lastPage = currentPage == maxPage || maxPage == 0
                ? string.Empty
                : $"<a class=\"w3-bar-item w3-hover-black w3-button\" href=\"/{controller}/{action}/{id}?page={maxPage}\">Last</a>";
            html.AppendLine(firstPage);
            html.AppendLine(/*string.Join("\n", */pageRange/*)*/);
            html.AppendLine(lastPage);
            html.AppendLine("</div></div>");
            return new HtmlString(html.ToString());
        }
    }
}