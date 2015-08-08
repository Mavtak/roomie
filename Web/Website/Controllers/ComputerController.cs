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

            var scriptObject = Script.Create(false, script);
            Database.Scripts.Add(scriptObject);

            var task = Task.Create(User, "Website", computer, scriptObject);

            Database.Tasks.Add(task);

            computer.UpdateLastScript(task.Script);
            Database.Computers.Update(computer);

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
            Database.Computers.Update(computer);
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
            Database.Computers.Update(computer);
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
        public ActionResult Create(string name)
        {
            var computer = Computer.Create(name, User);

            if (ModelState.IsValid)
            {
                Database.Computers.Add(computer);
                Database.SaveChanges();
                return RedirectToAction(
                    actionName: "Index"
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