using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                return _device.Thermostat.Temperature;
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
            _device.Thermostat.PollTemperature();
        }
    }
}
