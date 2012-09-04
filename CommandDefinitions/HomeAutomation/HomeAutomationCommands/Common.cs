using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;

using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Exceptions;
using WebCommunicator;

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
