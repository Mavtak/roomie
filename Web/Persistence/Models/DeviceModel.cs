﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.ToggleSwitches;
using Roomie.Web.Persistence.Helpers;
using BaseDevice = Roomie.Common.HomeAutomation.Device;
using BaseLocation = Roomie.Common.HomeAutomation.DeviceLocation;
using BaseNetwork = Roomie.Common.HomeAutomation.Network;

namespace Roomie.Web.Persistence.Models
{
    public class DeviceModel : IDeviceState, IHasDivId
    {
        [Key]
        public int Id { get; set; }

        public bool? IsConnected { get; set; }
        public DeviceType Type { get; set; }
        public virtual NetworkModel Network { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual DeviceLocationModel Location { get; set; }

        private string _notes;
        private bool _deserializedFromDatabase;
        public string Notes
        {
            get
            {
                return _notes;
            }
            set
            {
                if (!_deserializedFromDatabase && !string.Equals(_notes, value))
                {
                    var element = XElement.Parse(value);
                    var state = element.ToDeviceState();

                    Update(state);

                    _deserializedFromDatabase = true;
                }

                _notes = value;
            }
        }

        public void UpdateSerializedValue()
        {
            Notes = this.ToXElement().ToString();
        }

        public void Update(IDeviceState state)
        {
            //TODO: update more properties?
            IsConnected = state.IsConnected;
            
            if (state.ToggleSwitchState != null)
            {
                ToggleSwitch.Update(state.ToggleSwitchState);
            }

            if (state.DimmerSwitchState != null)
            {
                DimmerSwitch.Update(state.DimmerSwitchState);
            }

            if (state.ThermostatState != null)
            {
                Thermostat.Update(state.ThermostatState);
            }
        }

        private readonly ToggleSwitchModel _toggleSwitch;
        private readonly DimmerSwitchModel _dimmerSwitch;
        private readonly ThermostatModel _thermostat;

        public ToggleSwitchModel ToggleSwitch
        {
            get
            {
                return _toggleSwitch;
            }
        }

        public DimmerSwitchModel DimmerSwitch
        {
            get
            {
                return _dimmerSwitch;
            }
        }

        public ThermostatModel Thermostat
        {
            get
            {
                return _thermostat;
            }
        }

        public DeviceModel()
        {
            _toggleSwitch = new ToggleSwitchModel(this);
            _dimmerSwitch = new DimmerSwitchModel(this);
            _thermostat = new ThermostatModel(this);
        }

        #region LastPing

        public DateTime? LastPing { get; set; }

        public TimeSpan? TimeSinceLastPing
        {
            get
            {
                if (LastPing == null)
                    return null;
                return DateTime.UtcNow.Subtract(LastPing.Value);
            }
        }

        public void UpdatePing()
        {
            LastPing = DateTime.UtcNow;
        }

        #endregion

        public bool IsAvailable
        {
            get
            {
                return (IsConnected == true)
                    && (Network != null)
                    && (Network.IsAvailable == true);
            }
        }


        [NotMapped]
        DeviceLocation IDeviceState.Location
        {
            get
            {
                return Location;
            }
        }

        [NotMapped]
        Network IDeviceState.Network
        {
            get
            {
                return Network;
            }
        }

        [NotMapped]
        IToggleSwitchState IDeviceState.ToggleSwitchState
        {
            get
            {
                return ToggleSwitch;
            }
        }

        [NotMapped]
        IDimmerSwitchState IDeviceState.DimmerSwitchState
        {
            get
            {
                return DimmerSwitch;
            }
        }

        [NotMapped]
        IThermostatState IDeviceState.ThermostatState
        {
            get
            {
                return Thermostat;
            }
        }

        #region Object overrides

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var that = obj as DeviceModel;

            if (obj == null)
            {
                return false;
            }

            return this.Equals(that);
        }

        public bool Equals(DeviceModel that)
        {
            if (that == null)
            {
                return false;
            }

            if (this.Address != that.Address)
            {
                return false;
            }

            if (!NetworkModel.Equals(this.Network, that.Network))
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        public override string ToString()
        {
            var builder = new System.Text.StringBuilder();

            builder.Append("Device{Name='");
            builder.Append(Name);
            builder.Append("', Address='");
            builder.Append(Address);
            builder.Append("', Location-'");
            if (Location == null)
            {
                builder.Append("(unknown)");
            }
            else
            {
                builder.Append(Location.Name);
            }
            builder.Append("', Type='");
            builder.Append(Type);
            builder.Append(this.Describe());
            builder.Append("', Network='");
            builder.Append((Network != null) ? Network.Name : "(none}");
            builder.Append("'}");

            return builder.ToString();
        }

        #endregion

        #region HasId implementation

        public string DivId
        {
            get
            {
                return "device" + Id;
            }
        }

        #endregion
    }
}