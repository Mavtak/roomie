using System.Web.Mvc;
using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Website.Helpers;

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
    }
}
