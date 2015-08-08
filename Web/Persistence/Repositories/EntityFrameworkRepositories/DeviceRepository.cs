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
        private readonly IScriptRepository _scripts;
        private readonly ITaskRepository _tasks;

        public DeviceRepository(DbSet<EntityFrameworkDeviceModel> devices, IScriptRepository scripts, ITaskRepository tasks)
        {
            _devices = devices;
            _scripts = scripts;
            _tasks = tasks;
        }

        public EntityFrameworkDeviceModel Get(int id)
        {
            var result = _devices.Find(id);

            DeserializeDeviceState(result);
            result.ScriptRepository = _scripts;
            result.TaskRepository = _tasks;

            return result;
        }

        public EntityFrameworkDeviceModel Get(User user, int id)
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

        public EntityFrameworkDeviceModel[] Get(EntityFrameworkNetworkModel network)
        {
            var result = _devices.Where(x => x.Network.Id == network.Id).ToArray();

            foreach (var device in result)
            {
                DeserializeDeviceState(device);
                device.ScriptRepository = _scripts;
                device.TaskRepository = _tasks;
            }

            return result;
        }

        public void Add(EntityFrameworkDeviceModel device)
        {
            _devices.Add(device);
        }

        public void Remove(EntityFrameworkDeviceModel device)
        {
            _devices.Remove(device);
        }

        public void Update(int id, IDeviceState state)
        {
            var device = Get(id);

            device.Update(state, false);
            SerializeDeviceState(device);
        }

        private static void SerializeDeviceState(EntityFrameworkDeviceModel device)
        {
            device.Notes = device.ToXElement().ToString();
        }

        private static void DeserializeDeviceState(EntityFrameworkDeviceModel device)
        {
            if (string.IsNullOrEmpty(device.Notes))
            {
                return;
            }

            var element = XElement.Parse(device.Notes);
            var state = element.ToDeviceState();

            device.Update(state, true);
        }
    }
}
