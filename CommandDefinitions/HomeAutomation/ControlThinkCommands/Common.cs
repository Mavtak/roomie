using System;

namespace Roomie.CommandDefinitions.ZWave.ControlThinkCommands
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
