using System;
using System.Web;
using System.Web.Mvc;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;
using Roomie.Web.Persistence.Repositories;
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
            ExceptionLevel1();

            return View(viewName: "PlainText", model: "Exception handling went weird."); 
        }

        private void ExceptionLevel1()
        {
            try
            {
                ExceptionLevel2();
            }
            catch (Exception exception)
            {
                throw new Exception("This is too generic.", exception);
            }
        }

        private void ExceptionLevel2()
        {
            try
            {
                ExceptionLevel3();
            }
            catch (Exception exception)
            {
                throw new Exception("This exception is too middle.", exception);
            }
        }

        private void ExceptionLevel3()
        {
            throw new Exception("This exception is juuuust right!");
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

        public ActionResult DatabaseSchema()
        {
            var database = new EntityFrameworkRoomieDatabaseBackend("derp");
            var schema = database.GetObjectContext().CreateDatabaseScript();

            return View(viewName: "PlainText", model: schema); 
        }

        public ActionResult ResetDatabase()
        {
            throw new HttpException(404, "Not Found");

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
            throw new HttpException(404, "Not Found");

            string message = "working...";

            try
            {
                //Database.Seed();

                var computer = new ComputerModel
                {
                    Owner = Database.Users.Get(1),
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

        public ActionResult CleanUpScripts(int? timeout)
        {
            if (timeout < 1)
            {
                timeout = null;
            }

            var count = 0;

            DoWork.UntilTimeout(timeout ?? 5, () =>
                {
                    var iterationCount = Database.Scripts.Clean(Database.Tasks, Database.SavedScripts, Database.Computers);
                    Database.SaveChanges();

                    count += iterationCount;

                    return iterationCount == 0;
                });

            var message = count + " scripts cleaned up";

            return View(viewName: "PlainText", model: message);
        }
    }
}
