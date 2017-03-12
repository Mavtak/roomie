using System.Web;

namespace Roomie.Web.WebHook
{
    public static class WebhookUtilities
    {
        private static WebHookServer server;

        public static void StartServer()
        {
            server = new WebHookServer();
        }
        public static void ProcessRequest(HttpContext httpContext)
        {
            if (server == null)
            {
                WebHookServer tempServer = new WebHookServer();
                tempServer.RespondWithScreamingError(httpContext, "Server not started properly.");
                return;
            }

            server.ProcessRequest(httpContext);
        }
    }
}