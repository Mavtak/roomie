using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Roomie.Common.HomeAutomation.Thermostats.Fans
{
    public class ReadOnlyThermostatFanState : IThermostatFanState
    {
        public IEnumerable<ThermostatFanMode> SupportedModes { get; private set; }
        public ThermostatFanMode? Mode { get; private set; }
        public ThermostatFanCurrentAction? CurrentAction { get; private set; }

        private ReadOnlyThermostatFanState()
        {
        }

        public ReadOnlyThermostatFanState(IEnumerable<ThermostatFanMode> supportedModes, ThermostatFanMode? mode, ThermostatFanCurrentAction? currentAction)
        {
            SupportedModes = supportedModes;
            Mode = mode;
            CurrentAction = currentAction;
        }

        public static ReadOnlyThermostatFanState Blank()
        {
            var result = new ReadOnlyThermostatFanState
            {
                SupportedModes = new ThermostatFanMode[0]
            };

            return result;
        }

        public static ReadOnlyThermostatFanState CopyFrom(IThermostatFanState state)
        {
            var supportedModes = new List<ThermostatFanMode>();
            if (state.SupportedModes != null)
            {
                supportedModes.AddRange(state.SupportedModes);
            }

            var result = new ReadOnlyThermostatFanState
            {
                SupportedModes = supportedModes,
                Mode = state.Mode,
                CurrentAction = state.CurrentAction
            };

            return result;
        }

        public static ReadOnlyThermostatFanState Empty()
        {
            var result = new ReadOnlyThermostatFanState
                {
                    SupportedModes = new ThermostatFanMode[] {}
                };

            return result;
        }

        public static ReadOnlyThermostatFanState FromXElement(XElement element)
        {
            ThermostatFanCurrentAction? currentAction = null;
            var currentActionString = element.GetAttributeStringValue("CurrentAction");
            if (currentActionString != null)
            {
                currentAction = currentActionString.ToThermostatFanCurrentAction();
            }

            ThermostatFanMode? mode = null;
            var modeString = element.GetAttributeStringValue("Mode");
            if (modeString != null)
            {
                mode = modeString.ToThermostatFanMode();
            }

            var supportedModes = new List<ThermostatFanMode>();
            var supportedModesNode = element.Element("SupportedModes");
            if (supportedModesNode != null)
            {
                foreach (var modeElement in supportedModesNode.Elements())
                {
                    var supportedMode = modeElement.Value.ToThermostatFanMode();
                    supportedModes.Add(supportedMode);
                }
            }

            var result = new ReadOnlyThermostatFanState
                {
                    CurrentAction = currentAction,
                    Mode = mode,
                    SupportedModes = supportedModes
                };

            return result;
        }
    }
}
