using System.Collections.Generic;
using System.Threading.Tasks;
using Roomie.Common.Api.Models;

namespace Roomie.Common.Api.Client
{
    public interface IRoomieApiClient
    {
        Task<Response> Send(string repository, Request request);
        Task<Response<TResponseData>> Send<TResponseData>(string repository, Request request)
            where TResponseData : class;
    }
}
