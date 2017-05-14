using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Roomie.Common.Api.Client.Http
{
    public class JsonHttpClient : IJsonHttpClient
    {
        HttpClient _httpClient;

        public JsonHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Send(string verb, string[] pathSegments, IEnumerable<KeyValuePair<string, string>> queryParameters, IEnumerable<KeyValuePair<string, string>> headers, object body)
        {
            await Send<object>(
                verb,
                pathSegments,
                queryParameters,
                headers,
                body
            );
        }

        public async Task<TResult> Send<TResult>(string verb, string[] pathSegments, IEnumerable<KeyValuePair<string, string>> queryParameters, IEnumerable<KeyValuePair<string, string>> headers, object body)
            where TResult : class
        {
            var request = HttpClientUtilities.BuildRequest(
                verb,
                pathSegments,
                queryParameters,
                headers,
                JsonConvert.SerializeObject(body), "application/json"
            );
            var response = await _httpClient.SendAsync(request);
            var responseBodyString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TResult>(responseBodyString);

            return result;
        }
    }
}
