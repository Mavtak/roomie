using System.Web.Http;

namespace Roomie.Web.Website.Controllers.Api.CommandDocumentation
{
    public class CommandDocumentationController : BaseController
    {
        public object Post([FromBody] Request request)
        {
            var rpcRepository = new RpcCommandDocumentationRepository();

            return RpcRequestRouter.Route(rpcRepository, request);
        }
    }
}