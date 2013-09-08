using System.Xml.Linq;
using Roomie.Common.HomeAutomation.Thermostats.Cores;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Temperature;

namespace Roomie.Common.HomeAutomation.Thermostats
{
    public class ReadOnlyThermostatState : IThermostatState
    {
        public ITemperature Temperature { get; private set; }
        public IThermostatCoreState CoreState { get; private set; }
        public IThermostatFanState FanState { get; private set; }
        public ISetpointCollectionState SetpointStates { get; private set; }

        private ReadOnlyThermostatState()
        {
        }

        public ReadOnlyThermostatState(ITemperature temperature, IThermostatCoreState coreState, IThermostatFanState fanState, ISetpointCollectionState setpointStates)
        {
            Temperature = temperature;
            CoreState = CoreState;
            FanState = fanState;
            SetpointStates = setpointStates;
        }
        public static ReadOnlyThermostatState CopyFrom(IThermostatState state)
        {
            var result = new ReadOnlyThermostatState
            {
                Temperature = state.Temperature,
                CoreState = (state.CoreState == null) ? null : state.CoreState.Copy(),
                FanState = (state.FanState == null) ? null : state.FanState.Copy(),
                SetpointStates = (state.SetpointStates == null) ? null : state.SetpointStates.Copy(),
            };

            return result;
        }

        public static ReadOnlyThermostatState Empty()
        {
            var result = new ReadOnlyThermostatState
            {
                CoreState = ReadOnlyThermostatCoreState.Empty(),
                FanState = ReadOnlyThermostatFanState.Empty(),
                SetpointStates = ReadOnlySetpointCollection.Empty(),
            };

            return result;
        }

        public static ReadOnlyThermostatState FromXElement(XElement element)
        {
            ITemperature temperature = null;
            var temperatureString = element.GetAttributeStringValue("Temperature");
            if (temperatureString != null)
            {
                temperature = temperatureString.ToTemperature();
            }

            IThermostatCoreState coreState = null;
            var coreStateNode = element.Element("ThermostatCoreState");
            if (coreStateNode != null)
            {
                coreState = coreStateNode.ToThermostatCoreState();
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
                Temperature = temperature,
                CoreState = coreState,
                FanState = fanState,
                SetpointStates = setpoints
            };

            return result;
        }
    }
}
