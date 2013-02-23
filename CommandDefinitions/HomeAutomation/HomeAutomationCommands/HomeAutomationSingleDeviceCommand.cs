using Roomie.CommandDefinitions.HomeAutomationCommands.Attributes;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    [AutoConnectParameter]
    [DeviceParameter]
    public abstract class HomeAutomationSingleDeviceCommand : HomeAutomationNetworkCommand
    {
        protected override void Execute_HomeAutomationNetworkDefinition(HomeAutomationCommandContext context)
        {
            var scope = context.Scope;
            var network = context.Network;
            var networks = context.Networks;

            Device device = null;
            if (scope.VariableDefinedInThisScope("Device"))
            {
                var address = scope.GetValue("Device");

                device = networks.GetDevice(address, network);
            }

            var greaterContext = new HomeAutomationSingleDeviceContext(context)
            {
                Device = device
            };

            Execture_HomeAutomationSingleDeviceDefinition(greaterContext);
        }

        protected abstract void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context);
    }
}
