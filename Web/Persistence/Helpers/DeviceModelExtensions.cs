using System.Linq;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Helpers
{
    internal static class DeviceModelExtensions
    {
        public static void DoCommand(this DeviceModel device, string command, params string[] moreParams)
        {
            var deviceAddress = device.BuildVirtualAddress(true, true);
            var commandParameters = new []{deviceAddress}.Union(moreParams).ToArray();
            command = string.Format(command, commandParameters);

            var network = device.Network;
            var user = network.Owner;
            var computer = network.AttatchedComputer;

            var task = new TaskModel
            {
                Owner = user,
                Target = computer,
                Origin = "Web Interface",
                Script = new ScriptModel
                {
                    Mutable = false,
                    Text = command
                }
            };
            
            //TODO: does this get all of the tasks?  If so, find a better way!
            var tasks = user.Tasks;

            tasks.Add(task);
        }
    }
}
