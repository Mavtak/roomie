using System.Linq;
using System.Text;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Helpers
{
    internal static class DeviceModelExtensions
    {
        public static void DoCommand(this DeviceModel device, string name, params string[] additionalData)
        {
            var command = BuildCommand(device, name, additionalData);

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

        public static string BuildCommand(this DeviceModel device, string name, params string[] additionalData)
        {
            var deviceAddress = device.BuildVirtualAddress(true, true);
            var commandData = new []{"Device", deviceAddress}.Concat(additionalData).ToArray();

            var command = new StringBuilder();

            command.Append("HomeAutomation.");
            command.Append(name);

            var key = true;

            foreach (var entry in commandData)
            {
                if (key)
                {
                    command.Append(" ");
                }
                else
                {
                    command.Append("=\"");
                }

                command.Append(entry);

                if (!key)
                {
                    command.Append("\"");
                }

                key = !key;
            }

            return command.ToString();
        }
    }
}
