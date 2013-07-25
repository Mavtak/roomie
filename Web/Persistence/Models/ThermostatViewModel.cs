using System;
using System.Collections.Generic;
using System.Linq;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Temperature;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    public class ThermostatModel : IThermostat
    {
        public ITemperature Temperature { get; set; }
        IThermostatFanState IThermostatState.FanState
        {
            get
            {
                return Fan;
            }
        }
        public ThermostatFanModel Fan { get; private set; }
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
        ISetpointCollectionState IThermostatState.SetpointStates
        {
            get
            {
                return Setpoints;
            }
        }

        public ThermostatMode? Mode { get; set; }

        private DeviceModel _device;

        public ThermostatModel(DeviceModel device)
        {
            _device = device;

            //TODO: implement these
            Fan = new ThermostatFanModel(_device);
            SupportedModes = new List<ThermostatMode>();
            Setpoints = new ThermostatSetpointModel();
        }

        public void PollTemperature()
        {
            throw new NotImplementedException();
        }

        public void PollCurrentAction()
        {
            throw new NotImplementedException();
        }

        public void PollMode()
        {
            throw new NotImplementedException();
        }

        public void PollSupportedModes()
        {
            throw new NotImplementedException();
        }

        public void SetMode(ThermostatMode mode)
        {
            _device.DoCommand("HomeAutomation.SetThermostatMode Device=\"{0}\" ThermostatMode=\"{1}\"", mode.ToString());
        }

        internal void Update(IThermostatState data)
        {
            Temperature = data.Temperature;
            CurrentAction = data.CurrentAction;
            Mode = data.Mode;
            
            if (data.SupportedModes != null)
            {
                SupportedModes = data.SupportedModes.ToList();
            }

            if (data.FanState != null)
            {
                Fan.Update(data.FanState);
            }

            Setpoints = new ThermostatSetpointModel();

            if (data.SetpointStates != null)
            {
                foreach (var pair in data.SetpointStates.ToDictionary())
                {
                    Setpoints.Add(pair.Key, pair.Value);
                }
            }
        }
    }
}
