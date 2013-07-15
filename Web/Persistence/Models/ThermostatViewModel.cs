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
        IThermostatFanState IThermostatState.FanState
        {
            get
            {
                return Fan;
            }
        }
        public IThermostatFan Fan { get; private set; }
        public IEnumerable<ThermostatMode> SupportedModes { get; private set; }
        public ThermostatCurrentAction? CurrentAction { get; private set; }
        public ISetpointCollection Setpoints { get; private set; }
        ISetpointCollectionState IThermostatState.SetPointStates
        {
            get
            {
                return Setpoints;
            }
        }
        public ThermostatMode? Mode { get; private set; }

        private DeviceModel _device;

        public ThermostatViewModel(DeviceModel device)
        {
            _device = device;

            //TODO: implement these
            Fan = new FanModel();
            SupportedModes = new List<ThermostatMode>();
            Setpoints = new SetpointCollectionModel();
        }

        public void PollTemperature()
        {
            throw new NotImplementedException();
        }

        public void SetMode(ThermostatMode mode)
        {
            throw new NotImplementedException();
        }
    }
}
