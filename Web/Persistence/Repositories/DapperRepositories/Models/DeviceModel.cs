using System;
using System.Xml.Linq;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.DapperRepositories.Models
{
    public class DeviceModel
    {
        public string Address { get; set; }
        public int Id { get; set; }
        public bool? IsConnected { get; set; }
        public DateTime? LastPing { get; set; }
        public string Name { get; set; }
        public int? Network_Id { get; set; }
        public string Notes { get; set; }
        public string Type_Name { get; set; }
        
        public static DeviceModel FromRepositoryType(Device model)
        {
            var result = new DeviceModel
            {
                Address = model.Address,
                Id = model.Id,
                IsConnected = model.IsConnected,
                LastPing = model.LastPing,
                Name = model.Name,
                Network_Id = model.Network.Id,
                Notes = model.ToXElement().ToString(),
                Type_Name = model.Type.Name,
            };

            return result;
        }

        public Device ToRepositoryType(INetworkRepository networkRepository, IScriptRepository scriptRepository, ITaskRepository taskRepository)
        {
            IDeviceState state = null;

            if (!string.IsNullOrEmpty(Notes))
            {
                var element = XElement.Parse(Notes);
                state = element.ToDeviceState();
            }

            var result = new Device(
                address: Address,
                lastPing: LastPing,
                id: Id,
                name: Name,
                network: NetworkToRepositoryType(networkRepository),
                scripts: scriptRepository,
                state: state,
                tasks: taskRepository,
                type: DeviceType.GetTypeFromString(Type_Name)
            );

            return result;
        }

        private Network NetworkToRepositoryType(INetworkRepository networkRepository)
        {
            var id = Network_Id;

            if (id == null)
            {
                return null;
            }

            if (networkRepository == null)
            {
                new NetworkModel
                {
                    Id = id.Value
                }.ToRepositoryType(null, null);
            }

            return networkRepository.Get(id.Value);
        }
    }
}
