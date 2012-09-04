using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;

using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Exceptions;

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
