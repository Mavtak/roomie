using System;
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
                var entry = device.Manager.FormatData(value);

                result.AppendLine(entry);
            }

            return result.ToString();
        }

        // modified from Open Z-Wave demo code
        public static string FormatData(this ZWManager manager, ZWValueID value)
        {
            var result = new StringBuilder();

            result.AppendLine("CC   : " + FormatCommandClass(value.GetCommandClassId()));
            result.AppendLine("Type : " + value.GetType());
            result.AppendLine("Index: " + value.GetIndex());
            result.AppendLine("Inst : " + value.GetInstance());
            result.AppendLine("Value: " + manager.GetValue(value));
            result.AppendLine("Label: " + manager.GetValueLabel(value));
            result.AppendLine("Help : " + manager.GetValueHelp(value));
            result.AppendLine("Units: " + manager.GetValueUnits(value));

            return result.ToString();
        }

        public static string FormatCommandClass(byte commandClass)
        {
            var result = new StringBuilder();

            result.Append(commandClass);

            var value = ((CommandClass)commandClass).ToString();
            if (commandClass.ToString() != value)
            {
                result.Append(" (");
                result.Append(value);
                result.Append(")");
            }

            return result.ToString();

        }

        // modified from Open Z-Wave demo code
        public static string GetValue(this ZWManager manager, ZWValueID value)
        {
            switch (value.GetType())
            {
                case ZWValueID.ValueType.Bool:
                    bool r1;
                    manager.GetValueAsBool(value, out r1);
                    return r1.ToString();
                case ZWValueID.ValueType.Byte:
                    byte r2;
                    manager.GetValueAsByte(value, out r2);
                    return r2.ToString();
                case ZWValueID.ValueType.Decimal:
                    decimal r3;
                    manager.GetValueAsDecimal(value, out r3);
                    return r3.ToString();
                case ZWValueID.ValueType.Int:
                    Int32 r4;
                    manager.GetValueAsInt(value, out r4);
                    return r4.ToString();
                case ZWValueID.ValueType.List:
                    string[] r5;
                    manager.GetValueListItems(value, out r5);
                    string r6 = "";
                    foreach (string s in r5)
                    {
                        r6 += s;
                        r6 += "/";
                    }
                    return r6;
                case ZWValueID.ValueType.Schedule:
                    return "Schedule";
                case ZWValueID.ValueType.Short:
                    short r7;
                    manager.GetValueAsShort(value, out r7);
                    return r7.ToString();
                case ZWValueID.ValueType.String:
                    string r8;
                    manager.GetValueAsString(value, out r8);
                    return r8;
                default:
                    return "";
            }
        }
    }
}
