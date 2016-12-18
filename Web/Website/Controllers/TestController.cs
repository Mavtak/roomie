using System;
using System.Web.Mvc;
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

        public ActionResult Serialize()
        {
            var builder = new System.Text.StringBuilder();
            var writer = System.Xml.XmlWriter.Create(builder);            
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(User));
            serializer.Serialize(writer, User);

            return View(builder.ToString());
        }

        public ActionResult DatabaseSchema()
        {
            var database = new EntityFrameworkRoomieDatabaseBackend("derp");
            var schema = database.GetObjectContext().CreateDatabaseScript();

            return View(viewName: "PlainText", model: schema); 
        }

        public ActionResult CleanUpScripts(int? timeout)
        {
            if (timeout < 1)
            {
                timeout = null;
            }

            var deleted = 0;
            var skipped = 0;
            ListFilter filter = null;

            DoWork.UntilTimeout(timeout ?? 5, () =>
                {
                    var result = Database.Scripts.Clean(Database.Tasks, Database.Computers, filter);

                    deleted += result.Deleted;
                    skipped += result.Skipped;
                    filter = result.NextFilter;

                    return result.Done;
                });

            var message = deleted + " scripts cleaned up, " + skipped + " scripts skipped";

            return View(viewName: "PlainText", model: message);
        }
    }
}
