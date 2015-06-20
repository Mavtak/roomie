using Roomie.Common.Exceptions;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries
{
    public class CannotSetValueException : RoomieRuntimeException
    {
        private const string StaticMessage = "Could not set value";

        public CannotSetValueException()
            : base(StaticMessage)
        {
        }
    }
}
