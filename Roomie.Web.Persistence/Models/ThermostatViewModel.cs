using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Cores;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;

namespace Roomie.Web.Persistence.Models
{
    public class ThermostatModel : IThermostat
    {
        public ThermostatCoreModel Core { get; private set; }
        public ThermostatFanModel Fan { get; private set; }
        public ThermostatSetpointModel Setpoints { get; private set; }

        private Device _device;

        public ThermostatModel(Device device)
        {
            _device = device;

            Core = new ThermostatCoreModel(_device);
            Fan = new ThermostatFanModel(_device);
            Setpoints = new ThermostatSetpointModel(_device);
        }

        internal void Update(IThermostatState data)
        {
            Core.Update(data.CoreState ?? ReadOnlyThermostatCoreState.Blank());
            Fan.Update(data.FanState ?? ReadOnlyThermostatFanState.Blank());
            Setpoints.Update(data.SetpointStates ?? ReadOnlyThermostatSetpointCollection.Blank());
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
