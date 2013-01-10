using Roomie.Web.Persistence.Database;
using Roomie.Web.Persistence.Models;
using WebCommunicator;

namespace Roomie.Web.WebHook
{
    internal class WebHookContext : TransmissionContext
    {
        public ComputerModel Computer { get; set; }
        public WebHookSessionModel Session { get; set; }
        public UserModel User
        {
            get
            {
                return Computer.Owner;
            }
        }
        public RoomieDatabaseContext Database { get; set; }
    }
}
