using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Website.Helpers
{
    public interface IRoomieController
    {
        IRoomieDatabaseContext Database { get; set; }
        UserModel User { get; set; }
    }
}