using System.Collections.Generic;

namespace Roomie.Common.Api.Models
{
    public class Request
    {
        public string Action { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }
}
