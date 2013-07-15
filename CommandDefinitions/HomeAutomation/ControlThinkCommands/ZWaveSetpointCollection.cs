using System;
using System.Collections.Generic;
using ControlThink.ZWave.Devices.Specific;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Temperature;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    public class ZWaveSetpointCollection : ISetpointCollection
    {
        private Dictionary<SetpointType, ITemperature> _setpoints; 
        public ITemperature this[SetpointType setpointType]
        {
            get
            {
                return _setpoints[setpointType];
            }
        }

        public IEnumerable<SetpointType> AvailableSetpoints
        {
            get
            {
                return _setpoints.Keys;
            }
        }

        private ZWaveDevice _device;
        private GeneralThermostatV2 _thermostat;

        public ZWaveSetpointCollection(ZWaveDevice device)
        {
            _device = device;
            _thermostat = device.BackingObject as GeneralThermostatV2;

            _setpoints = new Dictionary<SetpointType, ITemperature>();

            if (_thermostat == null)
            {
                return;
            }

            SetCallbacks();
        }

        private void SetCallbacks()
        {
            _thermostat.ThermostatSetpointChanged += (sender, args) =>
            {
                var controlThinkSetpointType = args.ThermostatSetpointType;
                var controlThinkTemperature = args.Temperature;
                var roomieSetpointType = controlThinkSetpointType.ToRoomieType();
                var roomieTemperature = controlThinkTemperature.ToRoomieType();

                Update(roomieSetpointType, roomieTemperature);
                //TODO: raise event
            };
        }

        internal void Update(SetpointType setpointType, ITemperature temperature)
        {
            if (_setpoints.ContainsKey(setpointType))
            {
                _setpoints[setpointType] = temperature;
            }
            else
            {
                _setpoints.Add(setpointType, temperature);
            }
        }

        public void SetSetpoint(SetpointType setpointType, ITemperature temperature)
        {
            var controlThinkSetpointType = setpointType.ToControlThinkType();
            var controlThinkTemperature = temperature.ToControlThinkType();

            Action operation = () =>
            {
                _thermostat.ThermostatSetpoints[controlThinkSetpointType].Temperature = controlThinkTemperature;
            };

            _device.DoDeviceOperation(operation);
        }
    }
}
