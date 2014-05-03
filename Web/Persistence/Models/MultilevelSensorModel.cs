using System;
using Roomie.Common.HomeAutomation.MultilevelSensors;
using Roomie.Common.Measurements;

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
            throw new NotImplementedException();
        }

        public void Update(IMultilevelSensorState<TMeasurement> state)
        {
            Value = state.Value;
            TimeStamp = state.TimeStamp;
        }

    }
}
