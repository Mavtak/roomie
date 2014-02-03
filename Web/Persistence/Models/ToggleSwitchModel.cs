using System;
using System.Linq;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    public class ToggleSwitchModel : IBinarySwitch
    {
        private DeviceModel _device;

        public ToggleSwitchModel(DeviceModel device)
        {
            _device = device;
        }

        public void PowerOn()
        {
            _device.DoCommand("HomeAutomation.PowerOn Device=\"{0}\"");
        }

        public void PowerOff()
        {
            _device.DoCommand("HomeAutomation.PowerOff Device=\"{0}\"");
        }

        public void Poll()
        {
            //TODO: dependency inject the TaskQueue and make this method actually work!
            throw new System.NotImplementedException();
        }

        public BinarySwitchPower? Power { get; set; }

        public void Update(IBinarySwitchState state)
        {
            Power = state.Power;
        }
    }
}