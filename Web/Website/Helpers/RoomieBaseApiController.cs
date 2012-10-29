using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Roomie.Web.Helpers;
using System.Web.Http.Controllers;

namespace Roomie.Web.Website.Helpers
{
    public class RoomieBaseApiController : ApiController, IRoomieController
    {
        public RoomieDatabaseContext Database { get; set; }
        public new Models.UserModel User { get; set; }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Database = new RoomieDatabaseContext();

            User = UserUtilities.GetCurrentUser(Database);

            this.RefreshDatabaseHack();

            base.Initialize(controllerContext);
        }
    }
}