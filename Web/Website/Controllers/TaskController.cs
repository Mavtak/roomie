using System;
using System.Diagnostics;
using System.Web.Mvc;
using Roomie.Web.Persistence.Helpers;
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

            return View(tasks.Items);
        }

        public ActionResult Clean(int? timeout)
        {
            if (timeout < 1)
            {
                timeout = null;
            }

            var deleted = 0;
            var skipped = 0;
            ListFilter filter = null;

            DoWork.UntilTimeout(timeout ?? 5, () =>
                {
                    var result = Database.Tasks.Clean(User, filter);
                    Database.SaveChanges();

                    deleted += result.Deleted;
                    skipped += result.Skipped;
                    filter = result.NextFilter;

                    return result.Done;
                });

            return Content(deleted + " tasks cleaned up, " + skipped + " tasks skipped");
        }
    }
}
