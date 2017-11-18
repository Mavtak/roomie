using System.Web.Http;
using Roomie.Common.Api.Models;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api.Computer
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