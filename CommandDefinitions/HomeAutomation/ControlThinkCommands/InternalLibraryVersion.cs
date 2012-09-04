using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roomie.CommandDefinitions.ZWave.ControlThinkCommands
{
    internal static class InternalLibraryVersion
    {
        internal static Version GetLibraryVersion()
        {
            //automatically incremented by build script
            return new Version("1.0.864.0");
            //October 9, 2010:  Incremented build number from 82 to 650 because the auto-incrementing was never set up. >.<
        }
    }
}
