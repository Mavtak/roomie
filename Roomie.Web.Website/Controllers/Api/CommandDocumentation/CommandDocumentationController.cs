using System.Web.Http;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Website.Controllers.Api.CommandDocumentation
{
    public class CommandDocumentationController : BaseController
    {
        private RpcCommandDocumentationRepository _rpcCommandDocumentationRepository;

        public CommandDocumentationController()
        {
            _rpcCommandDocumentationRepository = new RpcCommandDocumentationRepository();
        }

        public Command[] Get()
        {
            return _rpcCommandDocumentationRepository.List();
        }
    }
}