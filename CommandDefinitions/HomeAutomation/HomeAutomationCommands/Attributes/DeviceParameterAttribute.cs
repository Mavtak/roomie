using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Attributes
{
    public class DeviceParameterAttribute : ParameterAttribute
    {
        //TODO: validation on an address?
        public DeviceParameterAttribute()
            : base("Device", "String")
        { }
    }
}
