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
            //TODO: remove this hack
            var scripts = new List<ScriptModel>(Database.Scripts);
            var computers = new List<ComputerModel>(Database.Computers);
            
            var tasks = (from t in Database.Tasks
                        where t.Owner.Id == User.Id
                        orderby t.Script.CreationTimestamp descending
                        select t).Skip((page-1)*count).Take(count);

            return View(tasks);
        }

        public ActionResult Details(int id)
        {
            return View(Database.Tasks.Find(id));
        }
    }
}
