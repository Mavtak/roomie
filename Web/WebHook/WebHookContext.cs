
using Roomie.Web.Helpers;
using Roomie.Web.Models;
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
