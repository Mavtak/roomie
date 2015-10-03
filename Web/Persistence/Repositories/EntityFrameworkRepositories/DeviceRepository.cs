using System;
using System.Data.Entity;
using System.Linq;
using System.Xml.Linq;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Repositories.EntityFrameworkRepositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DbSet<EntityFrameworkDeviceModel> _devices;
        private readonly DbSet<EntityFrameworkNetworkModel> _networks;
        private readonly IScriptRepository _scripts;
        private readonly ITaskRepository _tasks;

        public DeviceRepository(DbSet<EntityFrameworkDeviceModel> devices, DbSet<EntityFrameworkNetworkModel> networks, IScriptRepository scripts, ITaskRepository tasks)
        {
            _devices = devices;
            _networks = networks;
            _scripts = scripts;
            _tasks = tasks;
        }

        public Device Get(int id)
        {
            var model = _devices.Find(id);

            if(model == null)
            {
                return null;
            }

            var result = model.ToRepositoryType(_scripts, _tasks);

            return result;
        }

        public Device Get(User user, int id)
        {
            var result = Get(id);

            if (result == null)
            {
                return null;
            }

            if (result.Network == null)
            {
                throw new Exception("Network not set");
            }

            if (result.Network.Owner == null)
            {
                throw new Exception("Owner not set");
            }

            if (result.Network.Owner.Id != user.Id)
            {
                result = null;
            }

            return result;
        }

        public Device[] Get(Network network)
        {
            var models = _devices.Where(x => x.Network.Id == network.Id).ToArray();
            var results = models.Select(x =>
            {
                var result = x.ToRepositoryType(_scripts, _tasks);

                return result;
            }).ToArray();

            return results;
        }

        public void Add(Device device)
        {
            var model = EntityFrameworkDeviceModel.FromRepositoryType(device, _networks);

            _devices.Add(model);
        }

        public void Remove(Device device)
        {
            var model = _devices.Find(device.Id);

            _devices.Remove(model);
        }

        public void Update(Device device)
        {
            var model = _devices.Find(device.Id);

            model.Name = device.Name;
            model.Type = device.Type;
            model.Location = device.Location;
            model.Notes = EntityFrameworkDeviceModel.FromRepositoryType(device, _networks).Notes;
        }

        public void Update(int id, IDeviceState state)
        {
            //TODO: improve this
            var device = Get(id);

            device.Update(state, false);

            var model = _devices.Find(id);
            model.Notes = EntityFrameworkDeviceModel.FromRepositoryType(device, _networks).Notes;
        }
    }
}
