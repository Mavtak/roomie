using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BaseDeviceCollection = Roomie.Common.HomeAutomation.DeviceCollection;

namespace Roomie.Web.Models
{
    public class HomeAutomationDeviceCollection : BaseDeviceCollection
    {
        public HomeAutomationDeviceCollection(HomeAutomationNetworkModel network)
            : base(network)
        { }

        public HomeAutomationDeviceModel Find(HomeAutomationDeviceModel that)
        {
            foreach (HomeAutomationDeviceModel device in this)
            {
                if (device.Equals(that))
                {
                    return device;
                }
            }
            return null;
        }
    }
}
