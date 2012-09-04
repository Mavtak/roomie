using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebCommunicator;
using Roomie.Web.Models;
using Roomie.Web.Helpers;

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
