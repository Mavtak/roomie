using System.Xml.Linq;
using Roomie.Common.HomeAutomation.Thermostats.Cores;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;

namespace Roomie.Common.HomeAutomation.Thermostats
{
    public class ReadOnlyThermostatState : IThermostatState
    {
        public IThermostatCoreState CoreState { get; private set; }
        public IThermostatFanState FanState { get; private set; }
        public IThermostatSetpointCollectionState SetpointStates { get; private set; }

        private ReadOnlyThermostatState()
        {
        }

        public ReadOnlyThermostatState(IThermostatCoreState coreState, IThermostatFanState fanState, IThermostatSetpointCollectionState setpointStates)
        {
            CoreState = coreState;
            FanState = fanState;
            SetpointStates = setpointStates;
        }

        public static ReadOnlyThermostatState Blank()
        {
            var result = new ReadOnlyThermostatState
            {
                CoreState = ReadOnlyThermostatCoreState.Blank(),
                FanState =  ReadOnlyThermostatFanState.Blank(),
                SetpointStates = ReadOnlyThermostatSetpointCollection.Blank()
            };

            return result;
        }

        public static ReadOnlyThermostatState CopyFrom(IThermostatState state)
        {
            var result = new ReadOnlyThermostatState
            {
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
                SetpointStates = ReadOnlyThermostatSetpointCollection.Empty(),
            };

            return result;
        }

        public static ReadOnlyThermostatState FromXElement(XElement element)
        {
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

            IThermostatSetpointCollectionState setpoints = null;
            var setpointsNode = element.Element("Setpoints");
            if (setpointsNode != null)
            {
                setpoints = setpointsNode.ToSetpoints();
            }

            var result = new ReadOnlyThermostatState
            {
                CoreState = coreState,
                FanState = fanState,
                SetpointStates = setpoints
            };

            return result;
        }
    }
}
