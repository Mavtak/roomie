using System;
using System.Collections.Generic;
using System.Linq;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Cores;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    public class ThermostatModel : IThermostat
    {
        public ThermostatCoreModel Core { get; private set; }
        public ThermostatFanModel Fan { get; private set; }
        public ThermostatSetpointModel Setpoints { get; private set; }

        private DeviceModel _device;

        public ThermostatModel(DeviceModel device)
        {
            _device = device;

            Core = new ThermostatCoreModel(_device);
            Fan = new ThermostatFanModel(_device);
            Setpoints = new ThermostatSetpointModel(_device);
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

            if (data.CoreState != null)
            {
                Core.Update(data.CoreState);
            }

            if (data.FanState != null)
            {
                Fan.Update(data.FanState);
            }

            if (data.SetpointStates != null)
            {
                Setpoints = new ThermostatSetpointModel(_device);

                foreach (var pair in data.SetpointStates.ToDictionary())
                {
                    Setpoints.Add(pair.Key, pair.Value);
                }
            }
        }

        #region IThermostatState definitions

        IThermostatCoreState IThermostatState.CoreState
        {
            get
            {
                return Core;
            }
        }

        IThermostatFanState IThermostatState.FanState
        {
            get
            {
                return Fan;
            }
        }

        IThermostatSetpointCollectionState IThermostatState.SetpointStates
        {
            get
            {
                return Setpoints;
            }
        }

        #endregion

        #region IThermostatActions definitions

        IThermostatCoreActions IThermostatActions.CoreActions
        {
            get
            {
                return Core;
            }
        }
        IThermostatFanActions IThermostatActions.FanActions
        {
            get
            {
                return Fan;
            }
        }

        IThermostatSetpointCollectionActions IThermostatActions.SetpointActions
        {
            get
            {
                return Setpoints;
            }
        }

        #endregion

        #region IThermostat definitions

        IThermostatCore IThermostat.Core
        {
            get
            {
                return Core;
            }
        }
        IThermostatFan IThermostat.Fan
        {
            get
            {
                return Fan;
            }
        }
        IThermostatSetpointCollection IThermostat.Setpoints
        {
            get
            {
                return Setpoints;
            }
        }

        #endregion
    }
}
