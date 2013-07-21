using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Temperature;

namespace Roomie.Common.HomeAutomation.Thermostats
{
    public class ReadOnlyThermostatState : IThermostatState
    {
        public ITemperature Temperature { get; private set; }
        public IThermostatFanState FanState { get; private set; }
        public IEnumerable<ThermostatMode> SupportedModes { get; private set; }
        public ThermostatMode? Mode { get; private set; }
        public ThermostatCurrentAction? CurrentAction { get; private set; }
        public ISetpointCollectionState SetpointStates { get; private set; }

        public ReadOnlyThermostatState()
        {
        }

        public ReadOnlyThermostatState(ITemperature temperature, IThermostatFanState fanState, ISetpointCollectionState setpointStates, IEnumerable<ThermostatMode> supportedModes, ThermostatMode mode, ThermostatCurrentAction currentAction)
        {
            Temperature = temperature;
            FanState = fanState;
            SetpointStates = setpointStates;
            SupportedModes = supportedModes;
            Mode = mode;
            CurrentAction = currentAction;
        }
        public static ReadOnlyThermostatState CopyFrom(IThermostatState state)
        {
            var supportedModes = new List<ThermostatMode>();
            if (state.SupportedModes != null)
            {
                supportedModes.AddRange(state.SupportedModes);
            }

            IThermostatFanState fanState = null;
            if (state.FanState != null)
            {
                fanState = state.FanState.Copy();
            }

            ISetpointCollectionState setpoints = null;
            if (state.SetpointStates != null)
            {
                setpoints = state.SetpointStates.Copy();
            }

            var result = new ReadOnlyThermostatState
            {
                Temperature = state.Temperature,
                FanState = fanState,
                SupportedModes = supportedModes,
                Mode = state.Mode,
                CurrentAction = state.CurrentAction,
                SetpointStates = setpoints
            };

            return result;
        }

        public static ReadOnlyThermostatState Empty()
        {
            var result = new ReadOnlyThermostatState
            {
                FanState = ReadOnlyThermostatFanState.Empty(),
                SetpointStates = ReadOnlySetpointCollection.Empty(),
                SupportedModes = new ThermostatMode[] {}
            };

            return result;
        }

        public static ReadOnlyThermostatState FromXElement(XElement element)
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

            ITemperature temperature = null;
            var temperatureString = element.GetAttributeStringValue("Temperature");
            if (temperatureString != null)
            {
                temperature = temperatureString.ToTemperature();
            }

            IThermostatFanState fanState = null;
            var fanStateNode = element.Element("ThermostatFanState");
            if (fanStateNode != null)
            {
                fanState = fanStateNode.ToThermostatFanState();
            }

            ISetpointCollectionState setpoints = null;
            var setpointsNode = element.Element("Setpoints");
            if (setpointsNode != null)
            {
                setpoints = setpointsNode.ToSetpoints();
            }

            var result = new ReadOnlyThermostatState
            {
                CurrentAction = currentAction,
                Mode = mode,
                SupportedModes = supportedModes,
                Temperature = temperature,
                FanState = fanState,
                SetpointStates = setpoints
            };

            return result;
        }
    }
}
