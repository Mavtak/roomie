using System.Web.Http;

namespace Roomie.Web.Website.Controllers.Api.CommandDocumentation
{
    public class CommandDocumentationController : BaseController
    {
        private RpcCommandDocumentationRepository _rpcCommandDocumentationRepository;

        public CommandDocumentationController()
        {
            _rpcCommandDocumentationRepository = new RpcCommandDocumentationRepository();
        }

        public object Post([FromBody] Request request)
        {
            return RpcRequestRouter.Route(_rpcCommandDocumentationRepository, request);
        }
    }
}