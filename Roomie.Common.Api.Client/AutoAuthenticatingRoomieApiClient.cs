using Roomie.Common.Api.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Roomie.Common.Api.Client
{
    public class AutoAuthenticatingWebHookClient : IRoomieApiClient
    {
        private string _accessKey;
        private IRoomieApiClient _client;

        public AutoAuthenticatingWebHookClient(string accessKey, IRoomieApiClient client)
        {
            _accessKey = accessKey;
            _client = client;

        }

        public async Task Send(string resource, string action, object data)
        {
            await Send(resource, action, data);
        }

        public async Task<TResposneData> Send<TResposneData>(string resource, string action, object data)
            where TResposneData : class
        {
            var isCreatingSession = resource == "ApiSession" && action == "CreateSession";

            if (_accessKey == null && !isCreatingSession)
            {
                await _client.UpdateSessionToken(_accessKey);
            }

            try
            {
                return await _client.Send<TResposneData>(resource, action, data);
            }
            catch (RoomieApiClientException exception) when (exception?.Error?.Types?.Contains("invalid-session-token") == true && !isCreatingSession)
            {
                try
                {
                    await _client.UpdateSessionToken(_accessKey);
                }
                catch (RoomieApiClientException exception2)
                {
                    throw new RoomieApiClientException(
                        Error.Combine(
                            "An error occured while trying to re-authenticate with the server",
                            new[] { exception.Error, exception2.Error }
                        )
                    );
                }
            }

            return await _client.Send<TResposneData>(resource, action, data);
        }

        public void SetSessionToken(string sessionToken)
        {
            _client.SetSessionToken(sessionToken);
        }
    }
}
