
using System.Data.Entity;

namespace Roomie.Web.Persistence.Database
{
    public class RoomieDatabaseInitializer : CreateDatabaseIfNotExists<EntityFrameworkRoomieDatabaseBackend>
    {
        protected override void Seed(EntityFrameworkRoomieDatabaseBackend context)
        {
            //context.Seed();
        }
    }
}