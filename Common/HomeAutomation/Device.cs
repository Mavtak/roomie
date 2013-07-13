using System;
using System.Text;
using System.Xml.Linq;
using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.ToggleSwitches;

namespace Roomie.Common.HomeAutomation
{
    public abstract class Device : HomeAutomationEntity, IDeviceState
    {
        protected Network network { get; set; }
        public Network Network
        {
            get
            {
                return network;
            }
        }

        protected DeviceLocation location { get; set; }

        public DeviceLocation Location
        {
            get
            {
                return location;
            }
        }

        public Device(DeviceLocation location, Network network)
        {
            this.location = location;
            this.network = network;
        }

        public DeviceType Type { get; set; }
        public abstract IToggleSwitch ToggleSwitch { get; }
        public abstract IDimmerSwitch DimmerSwitch { get; }
        public abstract IThermostat Thermostat { get; }
        public int? MaxPower { get; set; }
        public bool? IsConnected { get; set; }
        
        //TODO: remove this
        protected int? power { get; set; }

        [Obsolete("Use the IDeviceState extension method instead.")]
        public override void FromXElement(XElement element)
        {
            CopyFrom(element.ToDeviceState());
        }

        public void CopyFrom(IDeviceState state)
        {
            Name = state.Name;
            Address = state.Address;
            location.Name = state.Location.Name;
            IsConnected = state.IsConnected;
            Type = state.Type;
            power = state.DimmerSwitchState.Power;
            MaxPower = state.DimmerSwitchState.MaxPower;
            //TODO: handle thermostat state and such
        }

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
    }
}
