using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Roomie.Web.Persistence.Helpers;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Website.Helpers;
using System.Runtime.Serialization;

namespace Roomie.Web.Website.Controllers.Api
{
    [ApiRestrictedAccessAttribute]
    [AutoSave]
    public class DeviceController : RoomieBaseApiController
    {
        // GET api/values
        public IEnumerable<SerializedDevice> Get()
        {
            var devices = User.GetAllDevices();

            return SerializedDevice.Convert(devices);
            
        }

        // GET api/values/5
        public SerializedDevice Get(int id)
        {
            var device = this.SelectDevice(id);

            return SerializedDevice.Convert(device);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            var device = this.SelectDevice(id);
            Database.Devices.Remove(device);
        }
    }

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
            return devices.Select(device => Convert(device));
        }
    }
}
