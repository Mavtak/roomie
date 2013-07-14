using System;
using System.Collections.Generic;
using ControlThink.ZWave.Devices.Specific;
using Roomie.Common.HomeAutomation.Thermostats.Fans;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    public class ZWaveThermostatFan : IThermostatFan
    {
        public IEnumerable<ThermostatFanMode> SupportedModes { get; internal set; }
        public ThermostatFanMode? Mode { get; internal set; }
        public ThermostatFanCurrentAction? CurrentAction { get; internal set; }

        private ZWaveDevice _device;
        private GeneralThermostatV2 _thermostat;

        public ZWaveThermostatFan(ZWaveDevice device)
        {
            _device = device;
            _thermostat = device.BackingObject as GeneralThermostatV2;

            SupportedModes = new List<ThermostatFanMode>();
        }

        public void SetMode(ThermostatFanMode fanMode)
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
