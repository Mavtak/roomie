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
        
        public static DeviceModel FromRepositoryType(Device repositoryModel)
        {
            if (repositoryModel == null)
            {
                return null;
            }

            var networkRepositoryModel = NetworkModel.FromRepositoryType(repositoryModel.Network);

            var result = new DeviceModel
            {
                Address = repositoryModel.Address,
                Id = repositoryModel.Id,
                IsConnected = repositoryModel.IsConnected,
                LastPing = repositoryModel.LastPing,
                Name = repositoryModel.Name,
                Network_Id = networkRepositoryModel?.Id,
                Notes = repositoryModel.ToXElement().ToString(),
                Type_Name = repositoryModel.Type.Name,
            };

            return result;
        }

        public Device ToRepositoryType(IRepositoryModelCache cache, INetworkRepository networkRepository, IScriptRepository scriptRepository, ITaskRepository taskRepository)
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
                network: NetworkModel.ToRepositoryType(cache, Network_Id, networkRepository),
                scripts: scriptRepository,
                state: state,
                tasks: taskRepository,
                type: DeviceType.GetTypeFromString(Type_Name)
            );

            cache?.Set(result.Id, result);

            return result;
        }
    }
}
