using System.Threading.Tasks;

namespace Roomie.Common.Api.Client
{
    public static class RoomieApiClientExtensions
    {
        public static async Task UpdateSessionToken(this IRoomieApiClient client, string apiKey)
        {
            var tokenResponse = await client.Send<Models.WebHookSession.CreateSession.Response>(
                resource:  "ApiSession",
                action:  "CreateSession",
                data: new Models.WebHookSession.CreateSession.Request
                {
                    AccessKey = apiKey,
                }
            );

            client.SetSessionToken(tokenResponse.Token);
        }
    }
}
