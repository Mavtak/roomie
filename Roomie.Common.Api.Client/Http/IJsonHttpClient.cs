using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roomie.Common.Api.Client.Http
{
    public interface IJsonHttpClient
    {
        Task Send(string verb, string[] pathSegments, IEnumerable<KeyValuePair<string, string>> queryParameters = null, IEnumerable<KeyValuePair<string, string>> headers = null, object body = null);
        Task<TResult> Send<TResult>(string verb, string[] pathSegments, IEnumerable<KeyValuePair<string, string>> queryParameters = null, IEnumerable<KeyValuePair<string, string>> headers = null, object body = null)
            where TResult : class;
    }
}
