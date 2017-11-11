using System.Web.Http;
using System.Web.Http.Controllers;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api.Computer
{
    [ApiRestrictedAccess]
    public class ComputerController : BaseController
    {
        private RpcComputerRepository _rpcComputerRepository;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            _rpcComputerRepository = new RpcComputerRepository(
                repositoryFactory: RepositoryFactory,
                user: User
            );
        }

        public object Post([FromBody] Request request)
        {
            return RpcRequestRouter.Route(_rpcComputerRepository, request);
        }
    }
}