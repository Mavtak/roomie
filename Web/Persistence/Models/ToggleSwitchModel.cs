using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.ToggleSwitches;

namespace Roomie.Web.Persistence.Models
{
    public class ToggleSwitchModel : IToggleSwitch
    {
        private DeviceModel _device;

        public ToggleSwitchModel(DeviceModel device)
        {
            _device = device;
        }

        public void PowerOn()
        {
            //TODO: dependency inject the TaskQueue and make this method actually work!
            throw new System.NotImplementedException();
        }

        public void PowerOff()
        {
            //TODO: dependency inject the TaskQueue and make this method actually work!
            throw new System.NotImplementedException();
        }

        public void Poll()
        {
            //TODO: dependency inject the TaskQueue and make this method actually work!
            throw new System.NotImplementedException();
        }

        public bool IsOff
        {
            get
            {
                return Utilities.IsOff(_device.Power);
            }
        }

        public bool IsOn
        {
            get
            {
                return Utilities.IsOn(_device.Power);
            }
        }
    }
}