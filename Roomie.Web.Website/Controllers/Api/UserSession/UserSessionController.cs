using System.Web.Http;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api.UserSession
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
