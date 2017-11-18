using System.Web.Http;
using Roomie.Common.Api.Models;

namespace Roomie.Web.Website.Controllers.Api.WebHookSession
{
    public class WebHookSessionController : BaseController
    {
        public object Post([FromBody] Request request)
        {
            var rpcRepository = new RpcWebHookSessionRepository(
                repositoryFactory: RepositoryFactory,
                user: User
            );

            return RpcRequestRouter.Route(rpcRepository, request);
        }
    }
}
