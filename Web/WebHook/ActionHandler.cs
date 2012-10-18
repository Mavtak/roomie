using System.Web;


namespace Roomie.Web.WebHook
{
    internal abstract class ActionHandler
    {
        private string cachedName = null;
        public string Name
        {
            get
            {
                if (cachedName == null)
                {
                    cachedName = GetType().Name;
                }

                return cachedName;
            }
        }

        internal string requestAddress
        {
            get
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
        }

        public abstract void ProcessMessage(WebHookContext webhookData);
    }
}