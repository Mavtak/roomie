using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Roomie.Common.HomeAutomation.Thermostats.Fans
{
    public static class ThermostatFanStateExtensions
    {
        public static ReadOnlyThermostatFanState Copy(this IThermostatFanState state)
        {
            return ReadOnlyThermostatFanState.CopyFrom(state);
        }

        public static string Describe(this IThermostatFanState state)
        {
            var result = new StringBuilder();

            if (state == null)
            {
                return result.ToString();
            }

            if (state.CurrentAction != null)
            {
                result.Append("Current Action: ");
                result.Append(state.CurrentAction);
            }

            if (state.Mode != null)
            {
                if (result.Length > 0)
                {
                    result.Append(", ");
                }

                result.Append("Mode: ");
                result.Append(state.Mode);
            }

            if (state.SupportedModes != null)
            {
                var modes = state.SupportedModes.ToArray();

                if (modes.Length > 0)
                {
                    if (result.Length > 0)
                    {
                        result.Append(", ");
                    }

                    result.Append("Supported Modes: ");

                    result.Append(string.Join(", ", modes));
                }
            }

            return result.ToString();
        }

        public static XElement ToXElement(this IThermostatFanState state, string nodeName="ThermostatFanState")
        {
            var result = new XElement(nodeName);

            if (state.CurrentAction != null)
            {
                result.Add(new XAttribute("CurrentAction", state.CurrentAction));
            }

            if (state.Mode != null)
            {
                result.Add(new XAttribute("Mode", state.Mode));
            }

            if (state.SupportedModes != null)
            {
                var supportedModes = state.SupportedModes.ToList();

                if (supportedModes.Count > 0)
                {
                    var supportedModesNode = new XElement("SupportedModes");
                    supportedModes.ForEach(x => supportedModesNode.Add(new XElement("SupportedMode", x)));
                    result.Add(supportedModesNode);
                }
            }

            return result;
        }

        public static ReadOnlyThermostatFanState ToThermostatFanState(this XElement element)
        {
            return ReadOnlyThermostatFanState.FromXElement(element);
        }

    }
}
