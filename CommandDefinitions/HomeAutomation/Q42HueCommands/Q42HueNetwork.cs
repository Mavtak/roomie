using System;
using System.Collections.Generic;
using System.Linq;
using Q42.HueApi;
using Roomie.CommandDefinitions.HomeAutomationCommands;

namespace Q42HueCommands
{
    public class Q42HueNetwork : Network
    {
        private const string AppName = "Roomie + Q42.HueApi";

        private readonly HueClient _client;
        private readonly string _appKey;
        private readonly List<Q42HueDevice> _devices; 

        public Q42HueNetwork(HomeAutomationNetworkContext context, string ip, string appKey) : base(context)
        {
            _appKey = appKey;
            _devices = new List<Q42HueDevice>();
            Devices = _devices;

            _client = new HueClient(ip);

            Connect();
        }

        internal void Connect()
        {
            _client.RegisterAsync(AppName, _appKey).Wait();
            _client.Initialize(_appKey);
            var bridge = _client.GetBridgeAsync().Result;

            Address = "Hue-" + bridge.Config.MacAddress.Replace(":", "");
            Name = Address;

            UpdateList(bridge);
            Load();
            Connected();
        }

        internal void UpdateList(Bridge bridge)
        {
            foreach (var light in bridge.Lights)
            {
                var existingDevice = _devices.FirstOrDefault(x => x.Address == light.Id);

                if (existingDevice == null)
                {
                    var device = new Q42HueDevice(this, light);

                    _devices.Add(device);
                }
                else
                {
                    existingDevice.UpdateBackingObject(light);
                }
            }
        }

        internal void UpdateList()
        {
            var bridge = _client.GetBridgeAsync().Result;
            UpdateList(bridge);
        }

        internal void SendCommand(LightCommand command, IEnumerable<string> lightList)
        {
            var task = _client.SendCommandAsync(command, lightList);
            task.Wait();
        }

        internal void SendCommand(LightCommand command, params Q42HueDevice[] devices)
        {
            SendCommand(command, devices.Select(x => x.Address));
        }

        internal void UpdateDevice(Q42HueDevice device)
        {
            var id = device.BackingObject.Id;
            var task = _client.GetLightAsync(id);
            var light = task.Result;
            light.Id = id;

            device.UpdateBackingObject(light);
        }

        public override Device RemoveDevice()
        {
            throw new NotImplementedException();
        }

        public override void RemoveDevice(Device device)
        {
            throw new NotImplementedException();
        }

        public override Device AddDevice()
        {
            throw new NotImplementedException();
        }
    }
}
