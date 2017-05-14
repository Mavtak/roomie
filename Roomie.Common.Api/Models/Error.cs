using System.Linq;

namespace Roomie.Common.Api.Models
{
    public class Error
    {
        public string[] Types { get; set; }
        public string Message { get; set; }

        public Error[] SubErrors { get; set; }

        public static Error Combine(string message, params Error[] errors)
        {
            var result = new Error
            {
                Types = new[] { "combined-errors" }
                            .Concat(errors.SelectMany(x => x.Types))
                            .Distinct()
                            .ToArray(),
                Message = "An error occured while trying to re-authenticate with the server",
                SubErrors = errors,
            };

            return result;
        }
    }
}
