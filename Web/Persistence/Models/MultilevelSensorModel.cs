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
        private DeviceModel _device;

        public MultilevelSensorModel(DeviceModel device)
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
                _device.DoCommand("HomeAutomation.PollPower Device=\"{0}\"");
                return;
            }

            if (type.IsAssignableFrom(typeof(ITemperature)))
            {
                _device.DoCommand("HomeAutomation.PollTemperature Device=\"{0}\"");
                return;
            }

            if (type.IsAssignableFrom(typeof(IHumidity)))
            {
                _device.DoCommand("HomeAutomation.PollHumidity Device=\"{0}\"");
                return;
            }

            if (type.IsAssignableFrom(typeof(IIlluminance)))
            {
                _device.DoCommand("HomeAutomation.PollIlluminance Device=\"{0}\"");
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
