﻿using System.Web.Mvc;
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
            if (timeout < 1)
            {
                timeout = null;
            }

            var count = Database.Tasks.Clean(User, () => Database.SaveChanges(), timeout ?? 5);

            return Content(count + " tasks cleaned up");
        }
    }
}
