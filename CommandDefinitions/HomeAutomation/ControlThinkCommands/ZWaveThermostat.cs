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

        public ZWaveThermostat(ZWaveDevice device)
        {
            _device = device;
        }

        public void SetSetpoint(SetpointType setpointType, ITemperature temperature)
        {
            var thermostat = _device.BackingObject as GeneralThermostatV2;
            var controlThinkSetpointType = setpointType.ToControlThinkType();
            var controlThinkTemperature = temperature.ToControlThinkType();

            Action operation = () =>
            {
                thermostat.ThermostatSetpoints[controlThinkSetpointType].Temperature = controlThinkTemperature;
            };

            _device.DoDeviceOperation(operation);
        }

        public void SetFanMode(FanMode fanMode)
        {
            var thermostat = _device.BackingObject as GeneralThermostatV2;
            var controlThinkFanMode = fanMode.ToControlThinkType();

            Action operation = () =>
            {
                thermostat.ThermostatFanMode = controlThinkFanMode;
            };

            _device.DoDeviceOperation(operation);

        }
    }
}
