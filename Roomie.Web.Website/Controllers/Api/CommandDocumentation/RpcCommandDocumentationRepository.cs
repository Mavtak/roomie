using Roomie.Common.Api.Models;
using Roomie.Desktop.Engine;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories.StaticRepositories;

namespace Roomie.Web.Website.Controllers.Api.CommandDocumentation
{
    public class RpcCommandDocumentationRepository
    {
        private CommandDocumentationRepository _commandDocumentationRepository;

        public RpcCommandDocumentationRepository()
        {
            var binPath = System.Web.Hosting.HostingEnvironment.MapPath("~/bin");
            var library = new RoomieCommandLibrary();
            library.AddCommandsFromPluginFolder(binPath);

            _commandDocumentationRepository = new CommandDocumentationRepository(library);
        }

        public Response<Command[]> List()
        {
            var result = _commandDocumentationRepository.Get();

            return Response.Create(result);
        }
    }
}