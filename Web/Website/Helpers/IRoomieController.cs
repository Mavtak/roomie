using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Website.Helpers
{
    public interface IRoomieController
    {
        IRoomieDatabaseContext Database { get; set; }
        EntityFrameworkUserModel User { get; set; }
    }
}