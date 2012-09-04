using System;
using System.Web.Mvc;
using Roomie.Web.Helpers;
using Roomie.Web.Models;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers
{
    public class ScriptController : RoomieBaseController
    {
        [UsersOnly]
        public ActionResult Index()
        {
            return View(User.SavedScripts);
        }

        public ActionResult Details(int id)
        {
            return View(SelectSavedScript(id));
        }

        [UsersOnly]
        public ActionResult New()
        {
            var model = new SavedScriptModel
            {
                Owner = User,
                Script = new ScriptModel
                {
                    
                }
            };

            return View(model);
        }

        [UsersOnly]
        [HttpPost]
        public ActionResult New(SavedScriptModel model, string returnUrl)
        {
            model.Owner = User;
            Database.SavedScripts.Add(model);
            Database.SaveChanges();

            if (Request.IsAjaxRequest())
            {
                return AjaxSuccess();
            }

            if (String.IsNullOrWhiteSpace(returnUrl))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return Redirect(returnUrl);
            }
        }

        public ActionResult Edit()
        {
            throw new NotImplementedException();
        }

        public ActionResult Run()
        {
            throw new NotImplementedException();
        }

    }
}
