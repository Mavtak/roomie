using System.Web;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Website.Helpers
{
    public static class RoomieControllerExtensions
    {
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