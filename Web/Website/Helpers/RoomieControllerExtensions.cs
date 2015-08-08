using System.Web;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Website.Helpers
{
    public static class RoomieControllerExtensions
    {
        public static EntityFrameworkDeviceModel SelectDevice(this IRoomieController controller, int id)
        {
            var database = controller.Database;
            var user = controller.User;

            var device = database.Devices.Get(user, id);

            if (device == null)
            {
                throw new HttpException(404, "Device not found");
            }

            return device;
        }

        public static EntityFrameworkNetworkModel SelectNetwork(this IRoomieController controller, int id)
        {
            var database = controller.Database;
            var user = controller.User;

            var network = database.Networks.Get(user, id);

            if (network == null)
            {
                throw new HttpException(404, "Network not found");
            }

            return network;
        }

        public static Computer SelectComputer(this IRoomieController controller, int id)
        {
            var database = controller.Database;
            var user = controller.User;

            var computer = database.Computers.Get(user, id);

            if (computer == null)
            {
                throw new HttpException(404, "Computer not found");
            }

            return computer;
        }

        public static Task SelectTask(this IRoomieController controller, int id)
        {
            var database = controller.Database;
            var user = controller.User;

            var task = database.Tasks.Get(user, id);

            if (task == null)
            {
                throw new HttpException(404, "Task not found");
            }

            return task;
        }

        public static void AddTask(this IRoomieController controller, Computer computer, string origin, string scriptText)
        {
            var database = controller.Database;
            var user = controller.User;

            var script = Script.Create(false, scriptText);
            database.Scripts.Add(script);

            var task = Task.Create(user, origin, computer, script);

            database.Tasks.Add(task);
        }
    }
}