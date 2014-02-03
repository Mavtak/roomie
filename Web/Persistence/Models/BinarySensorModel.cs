using System;
using Roomie.Common.HomeAutomation.BinarySensors;

namespace Roomie.Web.Persistence.Models
{
    public class BinarySensorModel : IBinarySensor
    {
        private DeviceModel _device;

        public BinarySensorModel(DeviceModel device)
        {
            _device = device;
        }

        public BinarySensorType? Type { get; set; }

        public bool? Value { get; set; }

        public void Poll()
        {
            throw new NotImplementedException();
        }

        public void Update(IBinarySensorState state)
        {
            Type = state.Type;
            Value = state.Value;
        }
    }
}
