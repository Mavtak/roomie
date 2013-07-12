using Roomie.Common.HomeAutomation;

namespace Roomie.Web.Persistence.Models
{
    public class DimmerSwitchModel : IDimmerSwitch
    {
        private DeviceModel _device;

        public DimmerSwitchModel(DeviceModel device)
        {
            _device = device;
        }

        public int? Power
        {
            get
            {
                return _device.Power;
            }
            set
            {
                //TODO: should this actually send a command?
                _device.Power = value;
            }
        }

        public int MaxPower
        {
            get
            {
                return _device.MaxPower;
            }
        }
        public int? Percentage
        {
            get
            {
                return Utilities.CalculatePowerPercentage(Power, MaxPower);
            }
        }

        public void Poll()
        {
            //TODO: dependency inject the TaskQueue and make this method actually work!
            throw new System.NotImplementedException();
        }
    }
}
