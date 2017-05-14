using Roomie.Common.Api.Client.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Roomie.Common.Api.Client
{
    public class RoomieApiClient : IRoomieApiClient
    {
        private IJsonHttpClient _jsonHttpClient;
        private string _sessionToken;

        public RoomieApiClient(IJsonHttpClient jsonHttpClient)
        {
            _jsonHttpClient = jsonHttpClient;
        }

        public RoomieApiClient(string apiUrl)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(apiUrl),
            };

            new JsonHttpClient(httpClient);
        }

        public async Task Send(string resource, string action, object data)
        {
            await Send<object>(resource, action, data);
        }

        public async Task<TResposneData> Send<TResposneData>(string resource, string action, object data)
            where TResposneData : class
        {
            var response = await _jsonHttpClient.Send<Models.Response<TResposneData>>(
                verb: "POST",
                pathSegments: new[] { resource },
                headers: new[] { new KeyValuePair<string, string>("x-roomie-webhook-session", _sessionToken) },
                body: data
            );

            if (response.Error != null)
            {
                throw new RoomieApiClientException(response.Error);
            }

            return response.Data;
        }

        public void SetSessionToken(string sessionToken)
        {
            _sessionToken = sessionToken;
        }
    }
}
