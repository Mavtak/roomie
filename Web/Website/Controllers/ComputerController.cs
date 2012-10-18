using System;
using System.Data.Entity;
using System.Web.Mvc;
using Roomie.Web.Models;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers
{
    public class ComputerController : RoomieBaseController
    {
        [UsersOnly]
        public ActionResult Index()
        {
            return View(User.Computers);
        }

        [UsersOnly]
        public ActionResult Details(int id)
        {
            return View(SelectComputer(id));
        }

        [HttpPost]
        [UsersOnly]
        [ValidateInput(false)]
        public ActionResult RunScript(int id, string script, string returnUrl)
        {
            var computer = SelectComputer(id);

            var task = new TaskModel
            {
                Origin = "Website",
                Owner = User,
                Target = computer,
                Expiration = DateTime.UtcNow.AddMinutes(1),
                Script = new ScriptModel
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
        [UsersOnly]
        public ActionResult RenewWebHookKeys(int id)
        {
            var computer = SelectComputer(id);

            computer.RenewWebhookKeys();
            Database.SaveChanges();

            return Json(new
            {
                success = true
            });
        }

        [HttpPost]
        [UsersOnly]
        public ActionResult DisableWebhook(int id)
        {
            var computer = SelectComputer(id);

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
        public ActionResult Create(ComputerModel computer)
        {
            computer.Owner = User;

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