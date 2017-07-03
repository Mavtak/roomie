using System;
using System.Collections.Generic;

namespace Roomie.Desktop.Engine.Commands
{
    public interface ICommandSpecification
    {
        string Name { get; }
        string Group { get; }
        string Description { get; }
        string Source { get; }
        string ExtensionName { get; }
        Version ExtensionVersion { get; }
        IEnumerable<RoomieCommandArgument> Arguments { get; }
    }
}
