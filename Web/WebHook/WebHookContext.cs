using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;
using WebCommunicator;

namespace Roomie.Web.WebHook
{
    internal class WebHookContext : TransmissionContext
    {
        public Computer Computer { get; set; }
        public WebHookSession Session { get; set; }
        public User User
        {
            get
            {
                return Computer.Owner;
            }
        }
        public IRoomieDatabaseContext Database { get; set; }
    }
}
