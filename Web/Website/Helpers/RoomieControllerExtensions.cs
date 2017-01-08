using System.Web;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Website.Helpers
{
    public static class RoomieControllerExtensions
    {
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