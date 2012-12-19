using System;
using System.Web.Mvc;
using Roomie.Web.Models;
using Roomie.Web.Website.Helpers;
using System.Linq;
using System.Collections.Generic;

namespace Roomie.Web.Website.Controllers
{
    [WebsiteRestrictedAccess]
    public class ScriptController : RoomieBaseController
    {
        public ActionResult Index()
        {
            var scripts = User.SavedScripts;

            return View(scripts);
        }

        public ActionResult Details(int id)
        {
            var script = this.SelectSavedScript(id);

            return View(script);
        }

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

        [HttpPost]
        public ActionResult New(string name, string text, string returnUrl)
        {
            var now = DateTime.UtcNow;

            var savedScript = new SavedScriptModel
            {
                ModificationTimestamp = null,
                Name = name,
                Owner = User,
                Script = new ScriptModel
                {
                    CreationTimestamp = now,
                    LastRunTimestamp = null,
                    Mutable = true,
                    RunCount = 0,
                    Text = text
                }
            };

            Database.SavedScripts.Add(savedScript);
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

        public ActionResult Edit(int id)
        {
            var savedScript = this.SelectSavedScript(id);

            return View(savedScript);
        }

        [HttpPost]
        public ActionResult Edit(int id, string name, string text, string returnUrl)
        {
            var savedScript = this.SelectSavedScript(id);

            if (name != null)
            {
                savedScript.Name = name;
            }

            if (text != null)
            {
                savedScript.Script.Text = text;
            }

            savedScript.ModificationTimestamp = DateTime.UtcNow;

            Database.SaveChanges();

            if (!String.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }


            return Json(new
                {
                    Id = savedScript.Id,
                    Name = name,
                    Text = text
                });

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var savedScript = this.SelectSavedScript(id);
            Database.SavedScripts.Remove(savedScript);
            Database.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Run(int id, int computerId, string returnUrl)
        {
            var savedScript = this.SelectSavedScript(id);
            var computer = this.SelectComputer(computerId);

            addTask(
                computer: computer,
                origin: "Scripts Menu",
                scriptText: savedScript.Script.Text
                );

            savedScript.Script.LastRunTimestamp = DateTime.UtcNow;
            savedScript.Script.RunCount++;

            Database.SaveChanges();

            if (!String.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Json(new
            {
                success = true
            });
        }

    }
}
