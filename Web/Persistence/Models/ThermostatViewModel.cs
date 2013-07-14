using System;
using System.Collections.Generic;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.Temperature;

namespace Roomie.Web.Persistence.Models
{
    public class ThermostatViewModel : IThermostat
    {
        public ITemperature Temperature { get; private set; }
        public IFanState Fan { get; private set; }
        public IEnumerable<ThermostatMode> SupportedModes { get; private set; }
        public ThermostatCurrentAction? CurrentAction { get; private set; }
        public ISetpointCollection SetPoints { get; private set; }
        public ThermostatMode? Mode { get; private set; }

        private DeviceModel _device;

        public ThermostatViewModel(DeviceModel device)
        {
            _device = device;

            //TODO: implement these
            Fan = new ReadOnlyFanState();
            SupportedModes = new List<ThermostatMode>();
            SetPoints = new ReadOnlySetPointCollection();
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
