using System;
using Roomie.Common.Exceptions;

namespace Roomie.Desktop.Engine.Exceptions
{
    [Serializable]
    class CommandNotFoundException : RoomieRuntimeException
    {
        private string commandFullName;
        public CommandNotFoundException(string commandFullName)
            : base("\"" + commandFullName + "\" command not found.")
        {
            this.commandFullName = commandFullName;
        }

        public string CommandFullName
        {
            get
            {
                return commandFullName;
            }
        }
    }
}
