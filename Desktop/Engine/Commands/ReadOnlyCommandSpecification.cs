using System;
using System.Collections.Generic;

namespace Roomie.Desktop.Engine.Commands
{
    public class ReadOnlyCommandSpecification : ICommandSpecification
    {
        public string Name { get; private set; }
        public string Group { get; private set; }
        public string Description { get; private set; }
        public string Source { get; private set; }
        public string ExtensionName { get; private set; }
        public Version ExtensionVersion { get; private set; }
        public IEnumerable<RoomieCommandArgument> Arguments { get; private set; }

        public ReadOnlyCommandSpecification(string name = null, string group = null, string description = null, string source = null, string extensionName = null, Version extensionVersion = null, IEnumerable<RoomieCommandArgument> arguments = null)
        {
            Name = name;
            Group = group;
            Description = description;
            Source = source;
            ExtensionName = extensionName;
            ExtensionVersion = extensionVersion;
            Arguments = arguments;
        }
    }
}
