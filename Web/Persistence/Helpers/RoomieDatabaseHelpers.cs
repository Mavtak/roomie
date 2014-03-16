using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Helpers
{
    public static class RoomieDatabaseHelpers
    {
        public static SavedScriptModel GetSavedScript(this IRoomieDatabaseContext database, UserModel user, int id)
        {
            var script = database.SavedScripts.Find(id);

            if (script == null || script.Owner != user)
            {
                return null;
            }

            return script;
        }

        public static ComputerModel GetComputer(this IRoomieDatabaseContext database, UserModel user, int id)
        {
            var computer = database.Computers.Find(id);

            if (computer == null || computer.Owner != user)
            {
                return null;
            }

            return computer;
        }
    }
}
