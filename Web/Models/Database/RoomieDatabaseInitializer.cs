
using System.Data.Entity;

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