using Roomie.Common.HomeAutomation.MultilevelSwitches;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    public class DimmerSwitchModel : IMultilevelSwitch
    {
        private Device _device;

        public DimmerSwitchModel(Device device)
        {
            _device = device;
        }

        public int? Power { get; set; }

        public int? MaxPower { get; set; }

        public void Poll()
        {
            //TODO: dependency inject the TaskQueue and make this method actually work!
            throw new System.NotImplementedException();
        }

        public void SetPower(int power)
        {
            _device.DoCommand("Dim", "Power", power.ToString());
        }

        public void Update(IMultilevelSwitchState state)
        {
            Power = state.Power;
            MaxPower = state.MaxPower;
        }
    }
}
