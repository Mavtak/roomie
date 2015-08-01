using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;
using WebCommunicator;

namespace Roomie.Web.WebHook
{
    internal class WebHookContext : TransmissionContext
    {
        public EntityFrameworkComputerModel Computer { get; set; }
        public EntityFrameworkWebHookSessionModel Session { get; set; }
        public EntityFrameworkUserModel User
        {
            get
            {
                return Computer.Owner;
            }
        }
        public IRoomieDatabaseContext Database { get; set; }
    }
}
