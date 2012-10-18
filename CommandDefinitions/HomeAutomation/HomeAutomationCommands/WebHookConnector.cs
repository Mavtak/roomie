

using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine;
using WebCommunicator;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    internal static class WebHookConnector
    {
        internal static bool WebHookPresent(RoomieCommandContext context)
        {
            return context.CommandLibrary.ContainsCommandGroup("WebHook");
        }

        internal static Message SendMessage(RoomieCommandContext context, Message outMessage)
        {
            //TODO: that's not too conclusive. 
            if (!WebHookPresent(context))
            {
                throw new RoomieRuntimeException("WebHook not present.");
            }

            Message response = WebHookCommands.Common.SendMessage(context.DataStore, outMessage);

            return response;
        }
    }
}
