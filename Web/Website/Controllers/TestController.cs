using System;
using System.Web.Mvc;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers
{
    public class TestController : RoomieBaseController
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            return View(this);
        }

        public ActionResult ThrowException()
        {
            throw new Exception("This is a test exception");
        }

        public ActionResult MarkupTest()
        {
            return View();
        }

        public ActionResult Serialize()
        {
            var builder = new System.Text.StringBuilder();
            var writer = System.Xml.XmlWriter.Create(builder);            
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(UserModel));
            serializer.Serialize(writer, User);

            return View(builder.ToString());
        }

        public ActionResult ResetDatabase()
        {
            string message = "working...";

            try
            {
                Database.Reset();
                Database.Dispose();
                Database = new RoomieDatabaseContext();
                Database.Seed();
                Database.SaveChanges();
                message += "\nsuccess!";
            }
            catch (Exception exception)
            {
                message += "\n" + exception.ToString();
            }

            return View(viewName: "PlainText", model: message);
        }

        public ActionResult SeedDatabase()
        {
            string message = "working...";

            try
            {
                //Database.Seed();

                var computer = new ComputerModel
                {
                    Owner = Database.Users.Find(1),
                    Name = "Test Computer"
                };
                Database.Computers.Add(computer);
                message += "\n" + computer;

                Database.SaveChanges();

                message += "\n" + computer;
                message += "\nsuccess!";
            }
            catch (Exception exception)
            {
                message += "\n" + exception.ToString();
            }

            return View(viewName: "PlainText", model: message);
        }
    }
}
