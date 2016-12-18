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

        public ActionResult DatabaseSchema()
        {
            var database = new EntityFrameworkRoomieDatabaseBackend("derp");
            var schema = database.GetObjectContext().CreateDatabaseScript();

            return View(viewName: "PlainText", model: schema); 
        }
    }
}
