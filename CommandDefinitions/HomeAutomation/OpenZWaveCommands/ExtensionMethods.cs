using System.Linq;
using System.Text;
using OpenZWaveDotNet;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public static class ExtensionMethods
    {
        public static string FormatData(this OpenZWaveDevice device)
        {
            var result = new StringBuilder();

            foreach (var value in device.Values)
            {
                var entry = value.FormatData();

                result.AppendLine(entry);
            }

            return result.ToString();
        }

        public static string FormatData(this OpenZWaveDeviceValue value)
        {
            var result = new StringBuilder();

            result.AppendLine("CC   : " + FormatCommandClass(value.CommandClass));
            result.AppendLine("Type : " + value.Type);
            result.AppendLine("Index: " + value.Index);
            result.AppendLine("Inst : " + value.Instance);
            result.AppendLine("Value: " + value.GetValue());
            result.AppendLine("Label: " + value.Label);
            result.AppendLine("Help : " + value.Help);
            result.AppendLine("Units: " + value.Units);

            return result.ToString();
        }

        public static string FormatCommandClass(CommandClass commandClass)
        {
            var result = new StringBuilder();

            result.Append((int)commandClass);

            var value = commandClass.ToString();
            if (((byte) commandClass).ToString() != value)
            {
                result.Append(" (");
                result.Append(value);
                result.Append(")");
            }

            return result.ToString();

        }

        public static string GetValue(this OpenZWaveDeviceValue value)
        {
            switch (value.Type)
            {
                case ZWValueID.ValueType.Bool:
                    return value.BooleanValue.ToString();

                case ZWValueID.ValueType.Byte:
                    return value.ByteValue.ToString();

                case ZWValueID.ValueType.Decimal:
                    return value.DecimalValue.ToString();

                case ZWValueID.ValueType.Int:
                    return value.IntValue.ToString();

                case ZWValueID.ValueType.List:
                    var selection = value.Selection;
                    var list = (value.ListItemsValue ?? new string[0])
                        .Select(x =>
                        {
                            if (string.Equals(x, selection))
                            {
                                return "*" + x + "*";
                            }

                            return x;
                        });

                    return "{" + string.Join("/", list) + "}";

                case ZWValueID.ValueType.Schedule:
                    return "Schedule";

                case ZWValueID.ValueType.Short:
                    return value.ShortValue.ToString();

                case ZWValueID.ValueType.String:
                    return value.StringValue;

                default:
                    return "";
            }
        }
    }
}
