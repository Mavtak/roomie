using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Helpers
{
    public static class RoomieDatabaseHelpers
    {
        public static DeviceModel GetDevice(this IRoomieDatabaseContext database, UserModel user, int id)
        {
            var device = database.Devices.Get(id);

            if (device == null || device.Network == null || device.Network.Owner != user)
            {
                return null;
            }

            return device;
        }

        public static NetworkModel GetNetwork(this IRoomieDatabaseContext database, UserModel user, int id)
        {
            var network = database.Networks.Get(id);

            if (network == null || network.Owner != user)
            {
                return null;
            }

            return network;
        }

        public static SavedScriptModel GetSavedScript(this IRoomieDatabaseContext database, UserModel user, int id)
        {
            var script = database.SavedScripts.Find(id);

            if (script == null || script.Owner != user)
            {
                return null;
            }

            return script;
        }

        public static ComputerModel GetComputer(this IRoomieDatabaseContext database, UserModel user, int id)
        {
            var computer = database.Computers.Find(id);

            if (computer == null || computer.Owner != user)
            {
                return null;
            }

            return computer;
        }

        public static TaskModel GetTask(this IRoomieDatabaseContext database, UserModel user, int id)
        {
            var task = database.Tasks.Get(id);

            if (task == null || task.Owner != user)
            {
                return null;
            }

            return task;
        }
    }
}
