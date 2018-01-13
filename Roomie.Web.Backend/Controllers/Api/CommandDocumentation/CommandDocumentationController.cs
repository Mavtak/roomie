using System.Web.Http;
using Roomie.Common.Api.Models;

namespace Roomie.Web.Backend.Controllers.Api.CommandDocumentation
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