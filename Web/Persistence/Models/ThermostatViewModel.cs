using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Temperature;

namespace Roomie.Web.Persistence.Models
{
    public class ThermostatViewModel : IThermostat
    {
        public ITemperature Temperature { get; set; }
        IThermostatFanState IThermostatState.FanState
        {
            get
            {
                return Fan;
            }
        }
        public FanModel Fan { get; private set; }
        IThermostatFan IThermostat.Fan
        {
            get
            {
                return Fan;
            }
        }
        public IEnumerable<ThermostatMode> SupportedModes { get; set; }
        public ThermostatCurrentAction? CurrentAction { get; set; }
        public ThermostatSetpointModel Setpoints { get; private set; }
        ISetpointCollection IThermostat.Setpoints
        {
            get
            {
                return Setpoints;
            }
        }
        ISetpointCollectionState IThermostatState.SetPointStates
        {
            get
            {
                return Setpoints;
            }
        }

        public ThermostatMode? Mode { get; set; }

        private DeviceModel _device;

        public ThermostatViewModel(DeviceModel device)
        {
            _device = device;

            //TODO: implement these
            Fan = new FanModel();
            SupportedModes = new List<ThermostatMode>();
            Setpoints = new ThermostatSetpointModel();
        }

        public void PollTemperature()
        {
            throw new NotImplementedException();
        }

        public void SetMode(ThermostatMode mode)
        {
            throw new NotImplementedException();
        }

        internal void Update(IThermostatState data)
        {
            Temperature = data.Temperature;
            CurrentAction = data.CurrentAction;
            Mode = data.Mode;

            Setpoints = new ThermostatSetpointModel();

            if (data.SetPointStates != null)
            {
                foreach (var pair in data.SetPointStates.ToDictionary())
                {
                    Setpoints.Add(pair.Key, pair.Value);
                }
            }
        }
    }
}
