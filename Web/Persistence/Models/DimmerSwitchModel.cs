﻿using Roomie.Common.HomeAutomation;

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
                _device.Power = value;
            }
        }

        public int? MaxPower
        {
            get
            {
                return _device.MaxPower;
            }
        }

        public void Poll()
        {
            //TODO: dependency inject the TaskQueue and make this method actually work!
            throw new System.NotImplementedException();
        }

        public void SetPower(int power)
        {
            //TODO: dependency inject the TaskQueue and make this method actually work!
        }
    }
}
