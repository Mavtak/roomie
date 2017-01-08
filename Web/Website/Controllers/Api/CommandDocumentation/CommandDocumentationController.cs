using Roomie.Desktop.Engine;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories.StaticRepositories;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api.CommandDocumentation
{
    public class CommandDocumentationController : RoomieBaseApiController
    {
        private CommandDocumentationRepository _commandDocumentationRepository;

        public CommandDocumentationController()
        {
            var binPath = System.Web.Hosting.HostingEnvironment.MapPath("~/bin");
            var library = new RoomieCommandLibrary();
            library.AddCommandsFromPluginFolder(binPath);

            _commandDocumentationRepository = new CommandDocumentationRepository(library);
        }

        public Command[] Get()
        {
            return _commandDocumentationRepository.Get();
        }
    }
}