using System;
using System.Collections.Generic;
using System.Web.Http;
using Roomie.Web.Persistence.Helpers;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api
{
    [ApiRestrictedAccessAttribute]
    [AutoSave]
    public class DeviceController : RoomieBaseApiController
    {
        // GET api/values
        public IEnumerable<SerializedDevice> Get()
        {
            var devices = Database.Devices.Get(User);

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
}
