using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
