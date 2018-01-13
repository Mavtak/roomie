using System.Web.Http;
using Roomie.Common.Api.Models;
using Roomie.Web.Backend.Helpers;

namespace Roomie.Web.Backend.Controllers.Api.Computer
{
    [ApiRestrictedAccess]
    public class ComputerController : BaseController
    {
        public object Post([FromBody] Request request)
        {
            var rpcRepository = new RpcComputerRepository(
                repositoryFactory: RepositoryFactory,
                user: User
            );

            return RpcRequestRouter.Route(rpcRepository, request);
        }
    }
}