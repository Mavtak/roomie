﻿using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    public class DimmerSwitchModel : IDimmerSwitch
    {
        private DeviceModel _device;

        public DimmerSwitchModel(DeviceModel device)
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
            _device.DoCommand("HomeAutomation.Poll Device=\"{0}\" Power=\"{1}\"", power.ToString());
        }

        public void Update(IDimmerSwitchState state)
        {
            Power = state.Power;
            MaxPower = state.MaxPower;
        }
    }
}
