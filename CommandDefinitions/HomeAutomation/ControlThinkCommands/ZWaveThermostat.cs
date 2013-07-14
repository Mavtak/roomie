using System;
using System.Collections.Generic;
using ControlThink.ZWave.Devices.Specific;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.Temperature;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    public class ZWaveThermostat : IThermostat
    {
        public ITemperature Temperature { get; private set; }
        IThermostatFanState IThermostatState.FanState
        {
            get
            {
                return Fan;
            }
        }
        public IThermostatFan Fan { get; private set; }
        public IEnumerable<ThermostatMode> SupportedModes { get; private set; }
        public ThermostatMode? Mode { get; private set; }
        public ThermostatCurrentAction? CurrentAction { get; private set; }
        public ISetpointCollection SetPoints { get; private set; }

        private ZWaveDevice _device;
        private GeneralThermostatV2 _thermostat;

        public ZWaveThermostat(ZWaveDevice device)
        {
            _device = device;
            _thermostat = device.BackingObject as GeneralThermostatV2;

            //TODO: implement these
            Fan = new ZWaveThermostatFan(device);
            SupportedModes = new List<ThermostatMode>();
            SetPoints = new ReadOnlySetPointCollection();

            if (_thermostat == null)
            {
                return;
            }

            SetCallbacks();
        }

        private void SetCallbacks()
        {
            _thermostat.ThermostatModeChanged += (sender, args) =>
                {
                     
                };

            _thermostat.ThermostatOperatingStateChanged += (sender, args) =>
                {

                };

            _thermostat.ThermostatSetpointChanged += (sender, args) =>
                {

                };

            _thermostat.ThermostatTemperatureChanged += (sender, args) =>
                {

                };
        }

        public void PollTemperature()
        {
            Action operation = () =>
            {
                var originalTemperature = Temperature;
                var controlThinkTemperature = _thermostat.ThermostatTemperature;

                Temperature = controlThinkTemperature.ToRoomieType();

                if (!Temperature.Equals(originalTemperature))
                {
                    IEventSource source = null;
                    var @event = DeviceEvent.TemperatureChanged(_device, source);
                    _device.AddEvent(@event);
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

        public void SetMode(ThermostatMode mode)
        {
            var controlThinkMode = mode.ToControlThinkType();

            Action operation = () =>
            {
                _thermostat.ThermostatMode = controlThinkMode;
            };

            _device.DoDeviceOperation(operation);
        }
    }
}
