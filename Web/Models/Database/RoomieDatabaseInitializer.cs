using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using Roomie.Web.Models;

namespace Roomie.Web.Helpers
{
    public class RoomieDatabaseInitializer : CreateDatabaseIfNotExists<RoomieDatabaseContext>
    {
        protected override void Seed(RoomieDatabaseContext context)
        {
            context.Seed();
        }
    }
}