using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        internal IEnumerable<Q42HueDevice> UpdateList(Bridge bridge)
        {
            var newDevices = new List<Q42HueDevice>();

            foreach (var light in bridge.Lights)
            {
                var existingDevice = _devices.FirstOrDefault(x => x.Address == light.Id);

                if (existingDevice == null)
                {
                    var device = new Q42HueDevice(this, light);

                    _devices.Add(device);
                    newDevices.Add(device);
                }
                else
                {
                    existingDevice.UpdateBackingObject(light);
                }
            }

            return newDevices;
        }

        internal IEnumerable<Q42HueDevice> UpdateList()
        {
            var bridge = _client.GetBridgeAsync().Result;
            return UpdateList(bridge);
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
            _client.SearchNewLightsAsync();

            //TODO: improve the Network model to allow this experience to be better
            Thread.Sleep(TimeSpan.FromMinutes(1)); // via http://www.developers.meethue.com/documentation/lights-api#12_get_new_lights

            var newLights = UpdateList();
            var result = newLights.FirstOrDefault();

            return result;
        }
    }
}
