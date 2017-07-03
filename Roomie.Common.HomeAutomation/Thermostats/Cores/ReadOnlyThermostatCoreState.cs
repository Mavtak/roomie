using System.Collections.Generic;
using System.Xml.Linq;

namespace Roomie.Common.HomeAutomation.Thermostats.Cores
{
    public class ReadOnlyThermostatCoreState : IThermostatCoreState
    {
        public IEnumerable<ThermostatMode> SupportedModes { get; private set; }
        public ThermostatMode? Mode { get; private set; }
        public ThermostatCurrentAction? CurrentAction { get; private set; }

        private ReadOnlyThermostatCoreState()
        {
        }

        public ReadOnlyThermostatCoreState(IEnumerable<ThermostatMode> supportedModes, ThermostatMode? mode, ThermostatCurrentAction? currentAction)
        {
            SupportedModes = supportedModes;
            Mode = mode;
            CurrentAction = currentAction;
        }

        public static ReadOnlyThermostatCoreState Blank()
        {
            var result = new ReadOnlyThermostatCoreState
            {
                SupportedModes = new ThermostatMode[0]
            };

            return result;
        }

        public static ReadOnlyThermostatCoreState CopyFrom(IThermostatCoreState state)
        {
            var supportedModes = new List<ThermostatMode>();
            if (state.SupportedModes != null)
            {
                supportedModes.AddRange(state.SupportedModes);
            }

            var result = new ReadOnlyThermostatCoreState
            {
                SupportedModes = supportedModes,
                Mode = state.Mode,
                CurrentAction = state.CurrentAction
            };

            return result;
        }

        public static ReadOnlyThermostatCoreState Empty()
        {
            var result = new ReadOnlyThermostatCoreState
                {
                    SupportedModes = new ThermostatMode[] {}
                };

            return result;
        }

        public static ReadOnlyThermostatCoreState FromXElement(XElement element)
        {
            ThermostatCurrentAction? currentAction = null;
            var currentActionString = element.GetAttributeStringValue("CurrentAction");
            if (currentActionString != null)
            {
                currentAction = currentActionString.ToThermostatCurrentAction();
            }

            ThermostatMode? mode = null;
            var modeString = element.GetAttributeStringValue("Mode");
            if (modeString != null)
            {
                mode = modeString.ToThermostatMode();
            }

            var supportedModes = new List<ThermostatMode>();
            var supportedModesNode = element.Element("SupportedModes");
            if (supportedModesNode != null)
            {
                foreach (var modeElement in supportedModesNode.Elements())
                {
                    var supportedMode = modeElement.Value.ToThermostatMode();
                    supportedModes.Add(supportedMode);
                }
            }

            var result = new ReadOnlyThermostatCoreState
                {
                    CurrentAction = currentAction,
                    Mode = mode,
                    SupportedModes = supportedModes
                };

            return result;
        }
    }
}
