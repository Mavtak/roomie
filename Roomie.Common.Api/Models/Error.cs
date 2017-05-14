using System.Linq;

namespace Roomie.Common.Api.Models
{
    public class Error
    {
        public string[] Types { get; set; }
        public string Message { get; set; }

        public Error[] SubErrors { get; set; }
    }
}
