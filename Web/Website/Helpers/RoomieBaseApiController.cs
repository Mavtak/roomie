﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Website.Helpers
{
    public class RoomieBaseApiController : ApiController, IRoomieController
    {
        public IRoomieDatabaseContext Database { get; set; }
        public new UserModel User { get; set; }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Database = new RoomieDatabaseContext();

            User = UserUtilities.GetCurrentUser(Database);

            base.Initialize(controllerContext);
        }
    }
}