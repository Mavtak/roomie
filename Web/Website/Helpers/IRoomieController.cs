using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Roomie.Web.Helpers;
using Roomie.Web.Models;

namespace Roomie.Web.Website.Helpers
{
    public interface IRoomieController
    {
        RoomieDatabaseContext Database { get; set; }
        UserModel User { get; set; }
    }
}