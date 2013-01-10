using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Helpers
{
    public static class RoomieDatabaseHelpers
    {
        public static DeviceModel GetDevice(this RoomieDatabaseContext database, UserModel user, int id)
        {
            var device = database.Devices.Find(id);

            if (device == null || device.Network == null || device.Network.Owner != user)
            {
                return null;
            }

            return device;
        }

        public static NetworkModel GetNetwork(this RoomieDatabaseContext database, UserModel user, int id)
        {
            var network = database.Networks.Find(id);

            if (network == null || network.Owner != user)
            {
                return null;
            }

            return network;
        }

        public static SavedScriptModel GetSavedScript(this RoomieDatabaseContext database, UserModel user, int id)
        {
            var script = database.SavedScripts.Find(id);

            if (script == null || script.Owner != user)
            {
                return null;
            }

            return script;
        }

        public static ComputerModel GetComputer(this RoomieDatabaseContext database, UserModel user, int id)
        {
            var computer = database.Computers.Find(id);

            if (computer == null || computer.Owner != user)
            {
                return null;
            }

            return computer;
        }

        public static TaskModel GetTask(this RoomieDatabaseContext database, UserModel user, int id)
        {
            var task = database.Tasks.Find(id);

            if (task == null || task.Owner != user)
            {
                return null;
            }

            return task;
        }
    }
}
