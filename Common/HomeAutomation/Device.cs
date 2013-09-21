using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.ToggleSwitches;

namespace Roomie.Common.HomeAutomation
{
    public abstract class Device : IDevice
    {
        public Network Network { get; protected set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ILocation Location { get; protected set; }

        public Device(ILocation location, Network network)
        {
            Location = location;
            Network = network;
        }

        public DeviceType Type { get; set; }
        public abstract IToggleSwitch ToggleSwitch { get; }
        public abstract IDimmerSwitch DimmerSwitch { get; }
        public abstract IThermostat Thermostat { get; }
        public bool? IsConnected { get; set; }

        public void Update(IDeviceState state)
        {
            Name = state.Name;
            Address = state.Address;
            Location.Update(state.Location);
            IsConnected = state.IsConnected;
            Type = state.Type;
            DimmerSwitch.Update(state.DimmerSwitchState);
            //TODO: handle thermostat state and such
        }

        #region IDeviceState

        IToggleSwitchState IDeviceState.ToggleSwitchState
        {
            get
            {
                return ToggleSwitch;
            }
        }
        IDimmerSwitchState IDeviceState.DimmerSwitchState
        {
            get
            {
                return DimmerSwitch;
            }
        }
        IThermostatState IDeviceState.ThermostatState
        {
            get
            {
                return Thermostat;
            }
        }

        #endregion

        #region IDeviceActions

        IToggleSwitchActions IDeviceActions.ToggleSwitchActions
        {
            get
            {
                return ToggleSwitch;
            }
        }

        IDimmerSwitchActions IDeviceActions.DimmerSwitchActions
        {
            get
            {
                return DimmerSwitch;
            }
        }

        IThermostatActions IDeviceActions.ThermostatActions
        {
            get
            {
                return Thermostat;
            }
        }

        #endregion
    }
}
