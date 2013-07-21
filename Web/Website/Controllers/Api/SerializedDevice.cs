using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Website.Controllers.Api
{
    [DataContract(Name="Device")]
    public class SerializedDevice
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public string Location { get; set; }
        [DataMember] public string Network { get; set; }
        [DataMember] public int? Power { get; set; }
        [DataMember] public bool? IsAvailable { get; set; }
        [DataMember] public int? MaxPower { get; set; }
        [DataMember] public string DivId { get; set; }

        public static SerializedDevice Convert(DeviceModel device)
        {
            return new SerializedDevice
                {
                    Name = device.Name,
                    Location = device.Location.Name,
                    Network = device.Network.Name,
                    Power = device.Power,
                    IsAvailable = device.IsAvailable,
                    MaxPower = device.MaxPower,
                    DivId = device.DivId
                };
        }

        public static IEnumerable<SerializedDevice> Convert(IEnumerable<DeviceModel> devices)
        {
            return devices.Select(device => Convert((DeviceModel) device));
        }
    }
}