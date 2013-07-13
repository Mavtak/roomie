using System;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.Temperature;

namespace Roomie.Web.Persistence.Models
{
    public class ThermostatViewModel : IThermostat
    {
        public ITemperature Temperature { get; private set; }

        private DeviceModel _device;

        public ThermostatViewModel(DeviceModel device)
        {
            _device = device;
        }

        public void PollTemperature()
        {
            throw new NotImplementedException();
        }

        public void SetFanMode(FanMode fanMode)
        {
            throw new NotImplementedException();
        }

        public void SetSetpoint(SetpointType setpointType, ITemperature temperature)
        {
            throw new NotImplementedException();
        }
    }
}
