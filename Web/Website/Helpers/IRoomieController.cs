using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Website.Helpers
{
    public interface IRoomieController
    {
        //TODO: change back to IRoomieDatabaseContext
        IRoomieDatabaseContext Database { get; set; }
        User User { get; set; }
    }
}