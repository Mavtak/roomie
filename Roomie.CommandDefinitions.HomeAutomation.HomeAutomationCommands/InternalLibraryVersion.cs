using System;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    internal static class InternalLibraryVersion
    {
        internal static Version GetLibraryVersion()
        {
            //automatically incremented by build script
            return new Version("1.0.2259.0");
        }
    }
}
