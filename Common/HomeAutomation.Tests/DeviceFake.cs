using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public override int? Power { get; set; }
    }
}
