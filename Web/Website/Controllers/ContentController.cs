using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Roomie.Web.Website.Controllers
{
    public class ContentController : Controller
    {
        public ActionResult Css(string file)
        {
            var builder = new StringBuilder();
            var writer = new StringWriter(builder);

            var fileContent = System.IO.File.OpenText(Request.MapPath(file)).ReadToEnd();

            var regex = new Regex("blerm");//TOO: make this work

            return new FileContentResult(Encoding.UTF8.GetBytes(builder.ToString()), "text/css");
        }

    }
}
