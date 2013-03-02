using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roomie.Web.Website.Helpers
{
    public static class HttpContextExtensions
    {

        public static CacheCookie GetCacheCookie(this HttpContextBase httpContext)
        {
            var key = typeof (CacheCookie);

            if (!httpContext.Items.Contains(key))
            {
                var cookie = CacheCookie.FromRequest(httpContext.Request);
                httpContext.Items.Add(key, cookie);
            }


            var result = httpContext.Items[key] as CacheCookie;

            return result;
        }
    }
}