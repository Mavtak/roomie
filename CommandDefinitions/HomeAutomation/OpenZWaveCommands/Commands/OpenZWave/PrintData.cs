using System.Linq;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Common.TextUtilities;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.Commands.OpenZWave
{
    public class PrintData : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var interpreter = context.Interpreter;
            var device = context.Device as OpenZWaveDevice;

            var headers = new[]
            {
                "Command Class",
                "Index",
                "Instance",
                "Value",
                "Units",
                "Type",
                "Label",
                "Help"
            };

            var values = device.Values
                               .OrderBy(x => x.Index)
                               .OrderBy(x => x.Instance)
                               .OrderBy(x => x.CommandClass.ToString());

            var rows = values.Select(TransformValue).ToArray();

            var rowsAndHeaders = new[] {headers}.Union(rows).ToArray();

            var columnWidths = new int[headers.Length];

            for (var i = 0; i < columnWidths.Length; i++)
            {
                columnWidths[i] = rowsAndHeaders.Max(x => x[i].Length);
            }


            var tableBuilder = new TextTable(columnWidths);

            interpreter.WriteEvent(tableBuilder.StartOfTable(headers));

            foreach (var row in rows)
            {
                interpreter.WriteEvent(tableBuilder.ContentLine(row));
            }

            interpreter.WriteEvent(tableBuilder.EndOfTable());
        }

        private static string[] TransformValue(OpenZWaveDeviceValue deviceValue)
        {
            var commandClass = deviceValue.CommandClass.ToString();
            var index = deviceValue.Index.ToString();
            var instance = deviceValue.Instance.ToString();
            var value = deviceValue.GetValue();
            var units = deviceValue.Units;
            var type = deviceValue.Type.ToString();
            var label = deviceValue.Label;
            var help = deviceValue.Help;

            var result = new[] {commandClass, index, instance, value, units, type, label, help};

            return result;
        }
    }
}
