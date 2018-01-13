using System.Web.Http;
using Roomie.Common.Api.Models;
using Roomie.Web.Backend.Helpers;

namespace Roomie.Web.Backend.Controllers.Api.Network
{
    [ApiRestrictedAccess]
    public class NetworkController : BaseController
    {
        public object Post([FromBody] Request request)
        {
            var rpcRepository = new RpcNetworkRepository(
                repositoryFactory: RepositoryFactory,
                user: User
            );

            return RpcRequestRouter.Route(rpcRepository, request);
        }
    }
}
