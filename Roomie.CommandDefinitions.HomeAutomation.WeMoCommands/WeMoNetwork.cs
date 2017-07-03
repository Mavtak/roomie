using System;
using System.Collections.Generic;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.CommandDefinitions.WeMoCommands.Sklose;

namespace Roomie.CommandDefinitions.WeMoCommands
{
    public class WeMoNetwork : Network
    {
        private List<WeMoDevice> _devices;

        public WeMoNetwork(HomeAutomationNetworkContext context)
            : base(context)
        {
            Address = "WeMo";
            Name = Address;

            _devices = new List<WeMoDevice>();
            Devices = _devices;
        }

        public void Connect(string host)
        {
            var binding = new System.ServiceModel.BasicHttpBinding();
            var address = new System.ServiceModel.EndpointAddress("http://" + host + ":49153/upnp/control/basicevent1");
            var client = new BasicServicePortTypeClient(binding, address);
            var device = new WeMoDevice(this, client, (_devices.Count + 1).ToString());

            _devices.Add(device);
        }

        public override Device AddDevice()
        {
            throw new NotImplementedException();
        }

        public override Device RemoveDevice()
        {
            throw new NotImplementedException();
        }

        public override void RemoveDevice(Device device)
        {
            throw new NotImplementedException();
        }
    }
}
