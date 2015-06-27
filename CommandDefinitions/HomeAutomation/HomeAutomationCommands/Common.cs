using System;

//TODO: Generalize NodeId to NetworkAddress
//TODO: Improve how devices are selected


namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public static class Common
    {
        public static Version LibraryVersion
        {
            get
            {
                return InternalLibraryVersion.GetLibraryVersion();
            }
        }
    }
}
