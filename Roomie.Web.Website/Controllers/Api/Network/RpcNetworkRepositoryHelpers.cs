using Roomie.Common.Api.Models;

namespace Roomie.Web.Website.Controllers.Api.Network
{
    public static class RpcNetworkRepositoryHelpers
    {
        public static Response CreateNotFoundError()
        {
            return Response.CreateError("Network not found", "not-found", "invalid-request");
        }
    }
}