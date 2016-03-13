using System.Web.Mvc;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers
{
    public class UserController : RoomieBaseController
    {

        [WebsiteRestrictedAccess]
        public ActionResult Edit()
        {
            return View(User);
        }

        
        [HttpPost]
        [WebsiteRestrictedAccess]
        public ActionResult Edit(User user)
        {
            this.User.Alias = user.Alias;
            Database.Users.Update(User);

            return View(User);
        }
    }
}
