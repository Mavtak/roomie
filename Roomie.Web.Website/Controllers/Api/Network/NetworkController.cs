using System.Web.Http;
using System.Web.Http.Controllers;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api.Network
{
    [ApiRestrictedAccess]
    public class NetworkController : BaseController
    {
        private RpcNetworkRepository _rpcNetworkRepository;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            _rpcNetworkRepository = new RpcNetworkRepository(
                repositoryFactory: RepositoryFactory,
                user: User
            );
        }

        public object Post([FromBody] Request request)
        {
            return RpcRequestRouter.Route(_rpcNetworkRepository, request);
        }
    }
}
