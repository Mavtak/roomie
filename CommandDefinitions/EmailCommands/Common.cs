using System;

namespace Roomie.CommandDefinitions.EmailCommands
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
