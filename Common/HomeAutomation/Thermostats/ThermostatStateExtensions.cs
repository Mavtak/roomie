using System.Linq;
using System.Text;
using System.Xml.Linq;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;

namespace Roomie.Common.HomeAutomation.Thermostats
{
    public static class ThermostatStateExtensions
    {
        public static ReadOnlyThermostatState Copy(this IThermostatState state)
        {
            return ReadOnlyThermostatState.CopyFrom(state);
        }

        public static string Describe(this IThermostatState state)
        {
            var result = new StringBuilder();

            if (state == null)
            {
                return result.ToString();
            }

            if (state.Temperature != null)
            {
                result.Append(state.Temperature);
            }

            var description = state.DescribeJustThermostat();
            if (!string.IsNullOrEmpty(description))
            {
                if (result.Length > 0)
                {
                    result.Append(", ");
                }

                result.Append(description);
            }

            var fanDescription = state.FanState.Describe();
            if (!string.IsNullOrEmpty(fanDescription))
            {
                if (result.Length > 0)
                {
                    result.Append(" | ");
                }

                result.Append("Fan: ");
                result.Append(fanDescription);
            }

            var setpointsDescription = state.SetpointStates.Describe();
            if (!string.IsNullOrEmpty(setpointsDescription))
            {
                if (result.Length > 0)
                {
                    result.Append(" | ");
                }

                result.Append("Setpoints: ");
                result.Append(setpointsDescription);
            }

            return result.ToString();
        }

        public static string DescribeJustThermostat(this IThermostatState state)
        {
            var result = new StringBuilder();

            if (state == null)
            {
                return result.ToString();
            }

            if (state.CurrentAction != null)
            {
                if (result.Length > 0)
                {
                    result.Append(" ");
                }

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

        public static XElement ToXElement(this IThermostatState state, string nodeName = "Thermostat")
        {
            var result = new XElement(nodeName);

            if (state.Temperature != null)
            {
                result.Add(new XAttribute("Temperature", state.Temperature));
            }

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

            if (state.FanState != null)
            {
                var fanNode = state.FanState.ToXElement();

                if (fanNode.Attributes().Any() || fanNode.Ancestors().Any())
                {
                    result.Add(fanNode);
                }
            }

            if (state.SetpointStates != null)
            {
                var setpointsNode = state.SetpointStates.ToXElement();

                if (setpointsNode.Attributes().Any() || setpointsNode.Elements().Any())
                {
                    result.Add(setpointsNode);
                }
            }

            return result;
        }

        public static ReadOnlyThermostatState ToThermostat(this XElement element)
        {
            return ReadOnlyThermostatState.FromXElement(element);
        }
    }
}
