using System;
using Roomie.Common.HomeAutomation.BinarySensors;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    public class BinarySensorModel : IBinarySensor
    {
        private EntityFrameworkDeviceModel _device;

        public BinarySensorModel(EntityFrameworkDeviceModel device)
        {
            _device = device;
        }

        public BinarySensorType? Type { get; set; }

        public bool? Value { get; set; }

        public DateTime? TimeStamp { get; set; }

        public void Poll()
        {
            _device.DoCommand("PollBinarySensor");
        }

        public void Update(IBinarySensorState state)
        {
            Type = state.Type;
            Value = state.Value;
            TimeStamp = state.TimeStamp;
        }
    }
}
