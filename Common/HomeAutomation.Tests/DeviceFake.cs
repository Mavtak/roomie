using System;

namespace Roomie.Common.HomeAutomation.Tests
{
    public class DeviceFake : Device
    {
        public DeviceFake(DeviceLocation location, Network network)
            : base(location, network)
        {
        }

        public DeviceFake(string name)
            : this(null, null)
        {
            Name = name;
            Address = name;
        }

        public override IToggleSwitch ToggleSwitch
        {
            get { throw new NotImplementedException(); }
        }

        public override IDimmerSwitch DimmerSwitch
        {
            get { throw new NotImplementedException(); }
        }

        public override IThermostat Thermostat
        {
            get { throw new NotImplementedException(); }
        }
    }
}
