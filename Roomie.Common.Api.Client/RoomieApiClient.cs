using Roomie.Common.Api.Client.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Roomie.Common.Api.Models;

namespace Roomie.Common.Api.Client
{
    public class RoomieApiClient : IRoomieApiClient
    {
        private IJsonHttpClient _jsonHttpClient;
        private string _accessKey;

        public RoomieApiClient(string apiUrl, string accessKey)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(apiUrl),
            };

            _jsonHttpClient = new JsonHttpClient(httpClient);

            _accessKey = accessKey;
        }

        public async Task<Response> Send(string repository, Request request)
        {
            var response = await Send<object>(repository, request);

            return new Response
            {
                Data = response.Data,
                Error = response.Error,
            };
        }

        public async Task<Response<TResponseData>> Send<TResponseData>(string repository, Request request)
            where TResponseData : class
        {
            var response = await _jsonHttpClient.Send<Response<TResponseData>>(
                verb: "POST",
                pathSegments: new[] { repository },
                headers: new[] { new KeyValuePair<string, string>("x-roomie-webhook-key", _accessKey) },
                body: request
            );

            return response;
        }
    }
}
