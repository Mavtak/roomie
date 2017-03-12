using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Repositories;

namespace Roomie.Web.Website.Controllers.Api.Network.Actions
{
    public class SyncWholeNetwork
    {
        private IComputerRepository _computerRepository;
        private IDeviceRepository _deviceRepository;
        private INetworkRepository _networkRepository;

        public SyncWholeNetwork(IComputerRepository computerRepository, IDeviceRepository deviceRepository, INetworkRepository networkRepository)
        {
            _computerRepository = computerRepository;
            _deviceRepository = deviceRepository;
            _networkRepository = networkRepository;
        }

        public Persistence.Models.Device[] Run(Persistence.Models.Computer computer, Persistence.Models.User user, Persistence.Models.Network network, IDeviceState[] sentDevices)
        {
            computer.UpdatePing();
            _computerRepository.Update(computer);

            network.UpdatePing(computer);
            _networkRepository.Update(network);

            var existingDevices = _deviceRepository.Get(network);

            UpdateDevices(sentDevices, existingDevices, network);

            var devicesToRemove = GetRemovedDevices(sentDevices, existingDevices);

            RemoveDevices(devicesToRemove);

            return existingDevices;
        }

        public Persistence.Models.Device[] Run(Persistence.Models.Computer computer, Persistence.Models.User user, Persistence.Models.Network network, IEnumerable<XElement> sentDevices)
        {
            return Run(computer, user, network, ProcessSentDevices(sentDevices, network));
        }

        private IDeviceState[] ProcessSentDevices(IEnumerable<XElement> sentDevices, Persistence.Models.Network network)
        {
            var result = sentDevices
                .Select(x => x.ToDeviceState())
                .Select(x => x.NewWithNetwork(network))
                .ToArray();

            return result;
        }

        private void UpdateDevices(IEnumerable<IDeviceState> sentDevices, IEnumerable<Persistence.Models.Device> registeredDevices, Persistence.Models.Network network)
        {
            // go through the devices from the client and update the entries in the database
            foreach (var sentDevice in sentDevices)
            {
                var registeredDevice = registeredDevices.FirstOrDefault(x => x.Address == sentDevice.Address && x.Network.Address == sentDevice.NetworkState.Address);

                if (registeredDevice == null)
                {
                    var newDevice = Persistence.Models.Device.Create(
                        address: sentDevice.Address,
                        isConnected: sentDevice.IsConnected,
                        name: sentDevice.Name,
                        network: network,
                        location: sentDevice.Location,
                        type: sentDevice.Type
                    );

                    newDevice.Update(sentDevice);

                    _deviceRepository.Add(newDevice);
                }
                else
                {
                    _deviceRepository.Update(registeredDevice.Id, sentDevice);
                }
            }
        }

        private IEnumerable<Persistence.Models.Device> GetRemovedDevices(IEnumerable<IDeviceState> sentDevices, IEnumerable<Persistence.Models.Device> registeredDevices)
        {
            foreach (var registeredDevice in registeredDevices)
            {
                if (!sentDevices.Any(d => d.Address == registeredDevice.Address))
                {
                    yield return registeredDevice;
                }
            }
        }

        private void RemoveDevices(IEnumerable<Persistence.Models.Device> devicesToRemove)
        {
            foreach (var device in devicesToRemove.ToArray())
            {
                _deviceRepository.Remove(device);
            }
        }
    }
}