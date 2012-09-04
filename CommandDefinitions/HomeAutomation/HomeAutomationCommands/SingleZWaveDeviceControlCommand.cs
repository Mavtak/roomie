using Roomie.CommandDefinitions.HomeAutomationCommands.Attributes;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    [AutoConnectParameter]
    [DeviceParameter]
    public abstract class SingleDeviceControlCommand : HomeAutomationNetworkCommand
    {
        public SingleDeviceControlCommand()
            : base()
        { }
    }
}
