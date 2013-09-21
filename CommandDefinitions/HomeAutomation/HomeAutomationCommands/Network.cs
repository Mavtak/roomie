using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Roomie.Common;
using Roomie.Common.HomeAutomation;
using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public abstract class Network : INetwork
    {
        internal HomeAutomationNetworkContext Context;

        public IEnumerable<Device> Devices { get; protected set; }

        public Network(HomeAutomationNetworkContext context)
        {
            Context = context;
            Devices = new List<Device>();
        }

        public abstract void Connect();
        public abstract void ScanForDevices();
        public abstract Device RemoveDevice();
        public abstract Device AddDevice();

        public void Save()
        {
            if (Name == null)
            {
                throw new Exception("Network name cannot be null");
            }

            var filename = Name + ".xml";

            var writer = XmlWriter.Create(filename);
            writer.WriteStartDocument();
            this.ToXElement().WriteTo(writer);
            writer.WriteEndDocument();
            writer.Close();
        }

        public void Load()
        {
            if (Name == null)
            {
                throw new Exception("Network name cannot be null");
            }

            var filename = Name + ".xml";

            if (!System.IO.File.Exists(filename))
                return;


            var rootElement = XElement.Load(filename);

            foreach (var element in rootElement.Elements())
            {
                var deviceAddress = element.GetAttributeStringValue("Address");
                if (Devices.ContainsAddress(deviceAddress))
                {
                    var device = Devices.GetDevice(deviceAddress);
                    device.Update(element.ToDeviceState());
                }
            }
        }

        #region INetworkDevice

        IEnumerable<IDevice> INetwork.Devices
        {
            get
            {
                return Devices;
            }
        }

        #endregion

        #region INetworkState

        public string Name { get; set; }
        public string Address { get; set; }

        IEnumerable<IDeviceState> INetworkState.DeviceStates
        {
            get
            {
                return Devices;
            }
        }

        #endregion

        #region INetworkDeviceActions

        IEnumerable<IDeviceActions> INetworkActions.DeviceActions
        {
            get
            {
                return Devices;
            }
        }

        #endregion
    }
}
