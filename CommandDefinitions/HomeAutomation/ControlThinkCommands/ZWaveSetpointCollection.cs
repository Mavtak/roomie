using System;
using System.Collections.Generic;
using System.Linq;
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

        public void PollSupportedSetpoints()
        {
            Action operation = () =>
            {
                var controlThinkSetpointTypes = _thermostat.SupportedThermostatSetpoints;

                var roomieSetpointTypes = controlThinkSetpointTypes.ToRoomieType();

                var setpointsToAdd = roomieSetpointTypes.Where(x => !_setpoints.ContainsKey(x));

                foreach (var setpoint in setpointsToAdd)
                {
                    _setpoints.Add(setpoint, null);
                }
            };

            _device.DoDeviceOperation(operation);
        }

        public void PollSetpointTemperatures()
        {
            Action operation = () =>
            {
                foreach (var setpointType in _setpoints.Keys.ToList())
                {
                    var controlThinkSetpointType = setpointType.ToControlThinkType();
                    var controlThinkTemperature = _thermostat.ThermostatSetpoints[controlThinkSetpointType].Temperature;
                    var temperature = controlThinkTemperature.ToRoomieType();

                    Update(setpointType, temperature);
                }
            };

            _device.DoDeviceOperation(operation);
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
