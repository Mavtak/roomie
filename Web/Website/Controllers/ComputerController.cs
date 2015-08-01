using System;
using System.Web.Mvc;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers
{
    [WebsiteRestrictedAccess]
    public class ComputerController : RoomieBaseController
    {
        public ActionResult Index()
        {
            var computers = Database.Computers.Get(User);

            return View(computers);
        }

        public ActionResult Details(int id)
        {
            var computer = this.SelectComputer(id);

            return View(computer);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult RunScript(int id, string script, string returnUrl)
        {
            var computer = this.SelectComputer(id);

            var task = new EntityFrameworkTaskModel
            {
                Origin = "Website",
                Owner = Database.Backend.Users.Find(User.Id),
                Target = computer,
                Expiration = DateTime.UtcNow.AddMinutes(1),
                Script = new EntityFrameworkScriptModel
                {
                    Text = script,
                    Mutable = false
                }
            };

            Database.Tasks.Add(task);

            computer.LastScript = task.Script;

            Database.SaveChanges();

            return Json(new
            {
                success = true
            });
        }

        [HttpPost]
        public ActionResult RenewWebHookKeys(int id)
        {
            var computer = this.SelectComputer(id);

            computer.RenewWebhookKeys();
            Database.SaveChanges();

            return Json(new
            {
                success = true
            });
        }

        [HttpPost]
        public ActionResult DisableWebhook(int id)
        {
            var computer = this.SelectComputer(id);

            computer.DisableWebhook();
            Database.SaveChanges();

            return Json(new
            {
                success = true
            });
        }

        public ActionResult Create()
        {
            //TODO: write this view
            return View();
        } 

        [HttpPost]
        public ActionResult Create(EntityFrameworkComputerModel computer)
        {
            computer.Owner = Database.Backend.Users.Find(User.Id);

            if (ModelState.IsValid)
            {
                Database.Computers.Add(computer);
                Database.SaveChanges();
                return RedirectToAction(
                    actionName: "Details",
                    routeValues: new
                    {
                        id = computer.Id,
                        name = computer.Name
                    }
                );  
            }

            return View(computer);
        }
        
        /*
        //
        // GET: /Computer/Edit/5
 
        public ActionResult Edit(int id)
        {
            ComputerModel computermodel = Database.Computers.Find(id);
            return View(computermodel);
        }

        //
        // POST: /Computer/Edit/5

        [HttpPost]
        public ActionResult Edit(ComputerModel computermodel)
        {
            if (ModelState.IsValid)
            {
                Database.Entry(computermodel).State = EntityState.Modified;
                Database.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(computermodel);
        }

        //
        // GET: /Computer/Delete/5
 
        public ActionResult Delete(int id)
        {
            ComputerModel computermodel = Database.Computers.Find(id);
            return View(computermodel);
        }

        //
        // POST: /Computer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            ComputerModel computermodel = Database.Computers.Find(id);
            Database.Computers.Remove(computermodel);
            Database.SaveChanges();
            return RedirectToAction("Index");
        }
         */

    }
}