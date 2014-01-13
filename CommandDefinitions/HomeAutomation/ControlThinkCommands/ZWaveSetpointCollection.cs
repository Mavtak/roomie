using System.Collections.Generic;
using System.Linq;
using ControlThink.ZWave.Devices.Specific;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Temperature;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    internal class ZWaveSetpointCollection : IThermostatSetpointCollection
    {
        private Dictionary<ThermostatSetpointType, ITemperature> _setpoints; 
        public ITemperature this[ThermostatSetpointType setpointType]
        {
            get
            {
                return _setpoints[setpointType];
            }
        }

        public IEnumerable<ThermostatSetpointType> AvailableSetpoints
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

            _setpoints = new Dictionary<ThermostatSetpointType, ITemperature>();

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

                if (roomieSetpointType.HasValue)
                {
                    Update(roomieSetpointType.Value, roomieTemperature);
                }

                IEventSource source = null;
                var @event = DeviceEvent.ThermostatSetpointsChanged(_device, source);
                _device.AddEvent(@event);
            };
        }

        internal void Update(ThermostatSetpointType setpointType, ITemperature temperature)
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
            var controlThinkSetpointTypes = _device.DoDeviceOperation(() => _thermostat.SupportedThermostatSetpoints);
            var roomieSetpointTypes = controlThinkSetpointTypes.ToRoomieType();
            var setpointsToAdd = roomieSetpointTypes.Where(x => !_setpoints.ContainsKey(x));

            foreach (var setpoint in setpointsToAdd)
            {
                _setpoints.Add(setpoint, null);
            }
        }

        public void PollSetpointTemperatures()
        {
            foreach (var setpointType in _setpoints.Keys.ToList())
            {
                var controlThinkSetpointType = setpointType.ToControlThinkType().Value;
                var controlThinkTemperature = _device.DoDeviceOperation(() => _thermostat.ThermostatSetpoints[controlThinkSetpointType].Temperature);
                var temperature = controlThinkTemperature.ToRoomieType();
                Update(setpointType, temperature);
            }
        }

        public void SetSetpoint(ThermostatSetpointType setpointType, ITemperature temperature)
        {
            var controlThinkSetpointType = setpointType.ToControlThinkType().Value;
            var controlThinkTemperature = temperature.ToControlThinkType().Value;
            _device.DoDeviceOperation(() => _thermostat.ThermostatSetpoints[controlThinkSetpointType].Temperature = controlThinkTemperature);
        }
    }
}
