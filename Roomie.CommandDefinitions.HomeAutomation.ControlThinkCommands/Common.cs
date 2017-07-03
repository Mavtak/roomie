using System;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    public class Common
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
