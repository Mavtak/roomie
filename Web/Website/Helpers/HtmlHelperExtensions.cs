using System.IO;
using System.Text;
using System.Web.Mvc;

namespace Roomie.Web.Website.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString InlineCss(this HtmlHelper htmlHelper, string path)
        {
            var result = new StringBuilder();

            path = htmlHelper.ViewContext.HttpContext.Request.MapPath(path);

            var file = File.OpenText(path);

            while (!file.EndOfStream)
            {
                var line = file.ReadLine().Trim();
                if (!line.StartsWith("//"))
                {
                    result.Append(line);
                }
            }

            return new MvcHtmlString(result.ToString());
        }
    }
}