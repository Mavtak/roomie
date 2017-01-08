using System;
using Roomie.Web.Persistence.Repositories;
using Roomie.Web.Website.Helpers;

namespace Roomie.Web.Website.Controllers.Api.Script
{
    [ApiRestrictedAccess]
    public class ScriptController : RoomieBaseApiController
    {
        public object Post(string action, int? timeout = null)
        {
            switch(action)
            {
                case "clean":
                    return Clean(timeout);

                default:
                    throw new NotImplementedException();
            }
        }

        private string Clean(int? timeout)
        {
            if (timeout < 1)
            {
                timeout = null;
            }

            var deleted = 0;
            var skipped = 0;
            ListFilter filter = null;

            DoWork.UntilTimeout(timeout ?? 5, () =>
            {
                var result = Database.Scripts.Clean(Database.Tasks, Database.Computers, filter);

                deleted += result.Deleted;
                skipped += result.Skipped;
                filter = result.NextFilter;

                return result.Done;
            });

            return deleted + " scripts cleaned up, " + skipped + " scripts skipped";
        }
    }
}