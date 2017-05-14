using System.Threading.Tasks;

namespace Roomie.Common.Api.Client
{
    public interface IRoomieApiClient
    {
        Task Send(string resource, string action, object data = null);
        Task<TResposneData> Send<TResposneData>(string resource, string action, object data = null)
            where TResposneData : class;
        void SetSessionToken(string sessionToken);

    }
}
