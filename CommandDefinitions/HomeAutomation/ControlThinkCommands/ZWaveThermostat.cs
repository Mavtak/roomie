using System;
using ControlThink.ZWave.Devices;
using ControlThink.ZWave.Devices.Specific;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.Temperature;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    public class ZWaveThermostat : IThermostat
    {
        private ZWaveDevice _device;
        private GeneralThermostatV2 _thermostat;
        public ITemperature Temperature { get; private set; }

        public ZWaveThermostat(ZWaveDevice device)
        {
            _device = device;
            _thermostat = device.BackingObject as GeneralThermostatV2;
        }

        public void PollTemperature()
        {
            Action operation = () =>
            {
                var controlThinkTemperature = _thermostat.ThermostatTemperature;

                Temperature = controlThinkTemperature.ToRoomieType();
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
