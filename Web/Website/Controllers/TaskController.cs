using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers
{
    [WebsiteRestrictedAccess]
    public class TaskController : RoomieBaseController
    {
        public ActionResult Index(int page = 1, int count = 50)
        {
            var tasks = Database.Tasks.List(User, page, count);

            return View(tasks);
        }
    }
}
