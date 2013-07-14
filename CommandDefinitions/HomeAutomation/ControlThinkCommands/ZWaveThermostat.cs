﻿using System;
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
        public IFanState Fan { get; private set; }
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
            Fan = new ReadOnlyFanState();
            SupportedModes = new List<ThermostatMode>();
            SetPoints = new ReadOnlySetPointCollection();
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

        public void SetFanMode(FanMode fanMode)
        {
            var controlThinkFanMode = fanMode.ToControlThinkType();

            Action operation = () =>
            {
                _thermostat.ThermostatFanMode = controlThinkFanMode;
            };

            _device.DoDeviceOperation(operation);

        }
    }
}
