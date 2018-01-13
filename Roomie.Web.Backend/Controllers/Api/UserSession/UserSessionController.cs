using System.Web.Http;
using Roomie.Common.Api.Models;
using Roomie.Web.Backend.Helpers;

namespace Roomie.Web.Backend.Controllers.Api.UserSession
{
    [ApiRestrictedAccess]
    public class UserSessionController : BaseController
    {
        public object Post([FromBody] Request request)
        {
            var rpcRepository = new RpcUserSessionRepository(
                repositoryFactory: RepositoryFactory,
                user: User
            );

            return RpcRequestRouter.Route(rpcRepository, request);
        }
    }
}
