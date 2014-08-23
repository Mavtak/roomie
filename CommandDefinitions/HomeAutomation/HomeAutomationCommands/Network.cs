using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.Triggers;

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

        public abstract Device RemoveDevice();
        public abstract void RemoveDevice(Device device);
        public abstract Device AddDevice();

        public void Log(string message)
        {
            var pool = Context.ThreadPool;
            var line = string.Format("{0}: {1}", Address, message);
            
            pool.Print(line);
        }

        protected void Connected()
        {
            Context.History.Add(NetworkEvent.Connected(this, null));
            Context.Triggers.CheckAndAct();
        }

        private string CalculateSavedFilename()
        {
            if (Name == null)
            {
                throw new Exception("Network name cannot be null");
            }

            var result = Name + ".xml";

            return result;
        }

        public void Save()
        {
            var filename = CalculateSavedFilename();
            var writer = XmlWriter.Create(filename);
            writer.WriteStartDocument();
            this.ToXElement().WriteTo(writer);
            writer.WriteEndDocument();
            writer.Close();
        }

        public void Load()
        {
            var filename = CalculateSavedFilename();

            if (!System.IO.File.Exists(filename))
            {
                return;
            }

            var element = XElement.Load(filename);
            var networkState = element.ToNetworkState();

            foreach (var deviceState in networkState.DeviceStates)
            {
                var address = deviceState.Address;
                if (Devices.ContainsAddress(address))
                {
                    var device = Devices.GetDevice(address);
                    device.Update(deviceState);
                }
            }
        }

        #region INetwork

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

        #region INetworkActions

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
