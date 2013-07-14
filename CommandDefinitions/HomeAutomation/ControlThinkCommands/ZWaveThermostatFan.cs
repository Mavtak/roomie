using System;
using System.Collections.Generic;
using ControlThink.ZWave.Devices.Specific;
using Roomie.Common.HomeAutomation.Thermostats.Fans;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    public class ZWaveThermostatFan : IFan
    {
        public IEnumerable<FanMode> SupportedModes { get; internal set; }
        public FanMode? Mode { get; internal set; }
        public FanCurrentAction? CurrentAction { get; internal set; }

        private ZWaveDevice _device;
        private GeneralThermostatV2 _thermostat;

        public ZWaveThermostatFan(ZWaveDevice device)
        {
            _device = device;
            _thermostat = device.BackingObject as GeneralThermostatV2;

            SupportedModes = new List<FanMode>();
        }

        public void SetMode(FanMode fanMode)
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
