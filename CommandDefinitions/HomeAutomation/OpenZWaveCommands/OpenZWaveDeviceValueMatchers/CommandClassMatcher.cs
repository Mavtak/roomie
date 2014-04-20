
namespace Roomie.CommandDefinitions.OpenZWaveCommands.OpenZWaveDeviceValueMatchers
{
    public class CommandClassMatcher : IOpenZWaveDeviceValueMatcher
    {
        private readonly CommandClass _commandClass;

        public CommandClassMatcher(CommandClass commandClass)
        {
            _commandClass = commandClass;
        }

        public bool Matches(OpenZWaveDeviceValue entry)
        {
            var result = entry.CommandClass == _commandClass;

            return result;
        }
    }
}
