using System;
using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.ToggleSwitches;

namespace Roomie.Common.HomeAutomation.Tests
{
    public class DeviceFake : Device
    {
        public DeviceFake(ILocation location, Network network)
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
