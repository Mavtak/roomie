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

            var device = database.Devices.Get(user, id);

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

            var network = database.Networks.Get(user, id);

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

            var script = database.SavedScripts.Get(user, id);

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

            var computer = database.Computers.Get(user, id);

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

            var task = database.Tasks.Get(user, id);

            if (task == null)
            {
                throw new HttpException(404, "Task not found");
            }

            return task;
        }

        public static void AddTask(this IRoomieController controller, ComputerModel computer, string origin, string scriptText)
        {
            var database = controller.Database;
            var user = controller.User;

            var task = new TaskModel
            {
                Owner = user,
                Target = computer,
                Origin = origin,
                Script = new ScriptModel
                {
                    Mutable = false,
                    Text = scriptText
                }
            };

            database.Tasks.Add(task);
        }
    }
}