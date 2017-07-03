using System.Linq;
using Roomie.Desktop.Engine;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.StaticRepositories
{
    public class CommandDocumentationRepository : ICommandDocumentationRepository
    {
        private readonly RoomieCommandLibrary _library;

        public CommandDocumentationRepository(RoomieCommandLibrary library)
        {
            _library = library;
        }

        public Command[] Get()
        {
            var result = _library
                .Select(Translate)
                .OrderBy(x => x.Name)
                .OrderBy(x => x.Group)
                .ToArray();

            return result;
        }

        private static Command.Argument Translate(RoomieCommandArgument argument)
        {
            return new Command.Argument(
                name: argument.Name,
                defaultValue: argument.DefaultValue,
                hasDefaultValue: argument.HasDefault,
                type: Translate(argument.Type)
            );
        }

        private static Command.Argument.TypeParameter Translate(IRoomieCommandArgumentType type)
        {
            return new Command.Argument.TypeParameter(
                description: type.ValidationMessage(null),
                name: type.Name
            );
        }

        private static Command Translate(RoomieCommand command)
        {
            return new Command(
                arguments: command.Arguments.Select(Translate).ToArray(),
                description: command.Description,
                group: command.Group,
                name: command.Name
            );
        }
    }
}
