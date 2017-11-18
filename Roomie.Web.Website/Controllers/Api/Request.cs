using System.Collections.Generic;

namespace Roomie.Web.Website.Controllers.Api
{
    public class Request
    {
        public string Action { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }
}