using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;

using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Exceptions;
using WebCommunicator;
using Roomie.CommandDefinitions.WebHookCommands;
using Roomie.Common.Exceptions;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    internal static class WebHookConnector
    {
        internal static bool WebHookPresent(RoomieEngine roomieController)
        {
            return roomieController.CommandLibrary.ContainsCommandGroup("WebHook");
        }

        internal static Message SendMessage(RoomieEngine roomieController, Message outMessage)
        {
            //TODO: that's not too conclusive. 
            if (!WebHookPresent(roomieController))
                throw new RoomieRuntimeException("WebHook not present.");

             Message response = WebHookCommands.Common.SendMessage(roomieController.DataStore, outMessage);

             return response;
        }
    }
}
