using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Roomie.Common.Api.Client.Http
{
    public static class HttpClientUtilities
    {
        public static HttpRequestMessage BuildRequest(string verbString, string[] pathSegments, IEnumerable<KeyValuePair<string, string>> queryParameters, IEnumerable<KeyValuePair<string, string>> headers, string body, string encoding)
        {
            var result = new HttpRequestMessage
            {
                Method = new HttpMethod(verbString),
                RequestUri = BuildUri(pathSegments, queryParameters),
                Content = new StringContent(body, Encoding.UTF8, encoding),
            };

            foreach (var header in headers)
            {
                result.Headers.Add(header.Key, header.Value);
            }

            return result;
        }

        public static Uri BuildUri(string[] pathSegments, IEnumerable<KeyValuePair<string, string>> queryParameters)
        {
            var path = string.Join(
                "/",
                pathSegments.Select(WebUtility.UrlEncode)
            );
            var querystring = string.Join(
                "&",
                queryParameters.Select(x => $"{WebUtility.UrlEncode(x.Key)}={WebUtility.UrlEncode(x.Value)}")
            );

            return new Uri(
                string.IsNullOrEmpty(querystring)
                    ? path
                    : $"{path}?{querystring}"
            );
        }
    }
}
