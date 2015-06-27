using System.Web;
using System.Web.Mvc;
using Roomie.Web.WebHook;

namespace Roomie.Web.Website.Helpers
{
    public class WebHookViewResult : ViewResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            WebhookUtilities.ProcessRequest(HttpContext.Current);//TODO: use context object
        }

    }
}
