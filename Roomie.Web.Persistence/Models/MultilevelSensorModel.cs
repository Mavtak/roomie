using System;
using Roomie.Common.HomeAutomation.MultilevelSensors;
using Roomie.Common.Measurements;
using Roomie.Common.Measurements.Humidity;
using Roomie.Common.Measurements.Illuminance;
using Roomie.Common.Measurements.Power;
using Roomie.Common.Measurements.Temperature;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    public class MultilevelSensorModel<TMeasurement> : IMultilevelSensor<TMeasurement>
        where TMeasurement : IMeasurement
    {
        private Device _device;

        public MultilevelSensorModel(Device device)
        {
            _device = device;
        }

        public TMeasurement Value { get; set; }
        public DateTime? TimeStamp { get; set; }

        public void Poll()
        {
            var type = typeof (TMeasurement);

            if (type.IsAssignableFrom(typeof(IPower)))
            {
                _device.DoCommand("PollPower");
                return;
            }

            if (type.IsAssignableFrom(typeof(ITemperature)))
            {
                _device.DoCommand("PollTemperature");
                return;
            }

            if (type.IsAssignableFrom(typeof(IHumidity)))
            {
                _device.DoCommand("PollHumidity");
                return;
            }

            if (type.IsAssignableFrom(typeof(IIlluminance)))
            {
                _device.DoCommand("PollIlluminance");
                return;
            }

            throw new NotImplementedException();
        }

        public void Update(IMultilevelSensorState<TMeasurement> state)
        {
            Value = state.Value;
            TimeStamp = state.TimeStamp;
        }

    }
}
