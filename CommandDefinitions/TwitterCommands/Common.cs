using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.TwitterCommands
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
