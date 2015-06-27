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

        public void SetPower(BinarySwitchPower power)
        {
            switch (power)
            {
                case BinarySwitchPower.On:
                    _device.DoCommand("PowerOn");
                    break;

                case BinarySwitchPower.Off:
                    _device.DoCommand("PowerOff");
                    break;
            }
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