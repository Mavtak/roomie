using ControlThink.ZWave.Devices.Specific;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Cores;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Measurements.Temperature;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    internal class ZWaveThermostat : IThermostat
    {
        public ITemperature Temperature { get; private set; }

        public IThermostatCore Core { get; private set; }
        public IThermostatFan Fan { get; private set; }
        private readonly ZWaveSetpointCollection _setpoints;
        public IThermostatSetpointCollection Setpoints
        {
            get
            {
                return _setpoints;
            }
        }

        private readonly ZWaveDevice _device;
        private readonly GeneralThermostatV2 _thermostat;

        public ZWaveThermostat(ZWaveDevice device)
        {
            _device = device;
            _thermostat = device.BackingObject as GeneralThermostatV2;

            Core = new ZWaveThermostatCore(device);
            Fan = new ZWaveThermostatFan(device);
            _setpoints = new ZWaveSetpointCollection(device);

            if (_thermostat == null)
            {
                return;
            }

            SetCallbacks();
        }

        private void SetCallbacks()
        {
            _thermostat.ThermostatTemperatureChanged += (sender, args) =>
                {
                    var controlThinkTemperature = args.ThermostatTemperature;
                    var roomieTemperature = controlThinkTemperature.ToRoomieType();

                    Temperature = roomieTemperature;

                    IEventSource source = null;
                    var @event = DeviceEvent.TemperatureChanged(_device, source);
                    _device.AddEvent(@event);
                };
        }

        public void PollTemperature()
        {
            var controlThinkTemperature = _device.DoDeviceOperation(() => _thermostat.ThermostatTemperature);
            Temperature = controlThinkTemperature.ToRoomieType();
        }

        #region IThermostatState definitions

        IThermostatCoreState IThermostatState.CoreState
        {
            get
            {
                return Core;
            }
        }

        IThermostatFanState IThermostatState.FanState
        {
            get
            {
                return Fan;
            }
        }

        IThermostatSetpointCollectionState IThermostatState.SetpointStates
        {
            get
            {
                return _setpoints;
            }
        }

        #endregion

        #region IThermostatActions definitions

        IThermostatCoreActions IThermostatActions.CoreActions
        {
            get
            {
                return Core;
            }
        }
        IThermostatFanActions IThermostatActions.FanActions
        {
            get
            {
                return Fan;
            }
        }

        IThermostatSetpointCollectionActions IThermostatActions.SetpointActions
        {
            get
            {
                return Setpoints;
            }
        }

        #endregion
    }
}
