using Roomie.CommandDefinitions.HomeAutomationCommands;

namespace Roomie.CommandDefinitions.ControlThinkCommands.Commands.ControlThink
{
    public class RemoveBrokenDevice : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var device = context.Device as ZWaveDevice;
            var network = context.Network as ZWaveNetwork;

            network.ZWaveController.Devices.Remove(device.BackingObject.NodeID);
        }
    }
}
