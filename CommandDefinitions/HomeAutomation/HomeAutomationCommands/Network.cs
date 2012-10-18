﻿using System;
using System.Xml;
using System.Xml.Linq;
using Roomie.Common;
using BaseNetwork = Roomie.Common.HomeAutomation.Network;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public abstract class Network : BaseNetwork
    {
        public string Address
        {
            get
            {
                return base.address;
            }
        }
        
        public Network()
            : base(null)
        {
            base.devices = new Roomie.Common.HomeAutomation.DeviceCollection(this);
        }

        public abstract void Connect();
        public abstract void ScanForDevices();
        public abstract Device RemoveDevice();
        public abstract Device AddDevice();

        //TODO: check to make sure that Name and Address are being used properly

        public DeviceCollection Devices
        {
            get
            {
                return (DeviceCollection)this.devices;
            }
            protected set
            {
                this.devices = value;
            }
        }

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
                if (devices.Contains(deviceAddress))
                {
                    var device = Devices[deviceAddress];
                    device.FromXElement(element);
                }
            }
        }

    }
}
