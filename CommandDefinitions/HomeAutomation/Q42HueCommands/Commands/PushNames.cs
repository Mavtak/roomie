using System.Linq;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Desktop.Engine.Commands;

namespace Q42HueCommands.Commands
{
    [Description("Set the name of each device on the Hue hub to match the last part of the device's location in Roomie and the device's name in Roomie.")]
    [Group("Q42Hue")]
    public class PushNames : HomeAutomationNetworkCommand
    {
        protected override void Execute_HomeAutomationNetworkDefinition(HomeAutomationCommandContext context)
        {
            var network = context.Network as Q42HueNetwork;

            foreach (var device in network.Devices)
            {
                PushName(device as Q42HueDevice);
            }
        }

        private static void PushName(Q42HueDevice device)
        {
            var name = device.Name;

            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            name = name.Trim();

            if (device.Location != null)
            {
                var lastLocationPart = device.Location.GetParts().LastOrDefault();
                
                if (lastLocationPart != null)
                {
                    name = lastLocationPart.Trim() + ": " + name;
                }
            }

            device.SetHueName(name);
        }
    }
}
