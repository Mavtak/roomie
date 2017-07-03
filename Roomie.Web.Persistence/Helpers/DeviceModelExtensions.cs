using System.Linq;
using System.Text;
using Roomie.Common.HomeAutomation;
using Roomie.Web.Persistence.Models;

namespace Roomie.Web.Persistence.Helpers
{
    internal static class DeviceModelExtensions
    {
        public static void DoCommand(this Device device, string name, params string[] additionalData)
        {
            var command = BuildCommand(device, name, additionalData);

            var network = device.Network;
            var user = network.Owner;
            var computer = network.AttatchedComputer;

            var script = Script.Create(false, command);
            device.ScriptRepository.Add(script);

            var task = Task.Create(user, "Web Interface", computer, script);
            
            device.TaskRepository.Add(task);
        }

        public static string BuildCommand(this Device device, string name, params string[] additionalData)
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
