using System.Diagnostics;
using System.Web.Mvc;
using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Website.Helpers;
using Roomie.Web.Persistence.Database;

namespace Roomie.Web.Website.Controllers
{
    [WebsiteRestrictedAccess]
    public class TaskController : RoomieBaseController
    {
        public ActionResult Index(ListFilter filter)
        {
            var tasks = Database.Tasks.List(User, filter);

            return View(tasks);
        }

        public ActionResult Clean(int? timeout)
        {
            if (timeout < 1 || timeout == null)
            {
                timeout = 5;
            }

            var count = 0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (stopwatch.Elapsed.TotalSeconds < timeout)
            {
                count += Database.Tasks.Clean(User);
                Database.SaveChanges();
            }
            

            return Content(count + " tasks cleaned up");
        }
    }
}
