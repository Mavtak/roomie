using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Roomie.Web.Persistence.Helpers;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Website.Controllers;

namespace Roomie.Web.Website.Helpers
{
    public static class RoomieControllerExtensions
    {
        public static DeviceModel SelectDevice(this IRoomieController controller, int id)
        {
            var database = controller.Database;
            var user = controller.User;

            var device = database.GetDevice(user, id);

            if (device == null)
            {
                throw new HttpException(404, "Device not found");
            }

            return device;
        }

        public static NetworkModel SelectNetwork(this IRoomieController controller, int id)
        {
            var database = controller.Database;
            var user = controller.User;

            var network = database.GetNetwork(user, id);

            if (network == null)
            {
                throw new HttpException(404, "Network not found");
            }

            return network;
        }

        public static SavedScriptModel SelectSavedScript(this IRoomieController controller, int id)
        {
            var database = controller.Database;
            var user = controller.User;

            var script = database.GetSavedScript(user, id);

            if (script == null)
            {
                throw new HttpException(404, "Script not found");
            }

            return script;
        }

        public static ComputerModel SelectComputer(this IRoomieController controller, int id)
        {
            var database = controller.Database;
            var user = controller.User;

            var computer = database.GetComputer(user, id);

            if (computer == null)
            {
                throw new HttpException(404, "Computer not found");
            }

            return computer;
        }

        public static TaskModel SelectTask(this IRoomieController controller, int id)
        {
            var database = controller.Database;
            var user = controller.User;

            var task = database.GetTask(user, id);

            if (task == null)
            {
                throw new HttpException(404, "Task not found");
            }

            return task;
        }
    }
}