using System.Web.Http;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api.Network
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
