using System;
using Roomie.Common.HomeAutomation.MultilevelSensors;
using Roomie.Common.Measurements.Temperature;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    internal class ZWaveTemperatureSensor : IMultilevelSensor<ITemperature>
    {
        private readonly ZWaveDevice _device;

        public ZWaveTemperatureSensor(ZWaveDevice device)
        {
            _device = device;
        }

        public ITemperature Value
        {
            get
            {
                return _device.ZWaveThermostat.Temperature;
            }
        }

        public DateTime? TimeStamp
        {
            get
            {
                return null;
            }
        }

        public void Poll()
        {
            _device.ZWaveThermostat.PollTemperature();
        }
    }
}
