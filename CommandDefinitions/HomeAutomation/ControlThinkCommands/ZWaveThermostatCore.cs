using System.Collections.Generic;
using ControlThink.ZWave.Devices.Specific;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Cores;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    internal class ZWaveThermostatCore : IThermostatCore
    {
        public IEnumerable<ThermostatMode> SupportedModes { get; internal set; }
        public ThermostatMode? Mode { get; internal set; }
        public ThermostatCurrentAction? CurrentAction { get; internal set; }

        private ZWaveDevice _device;
        private GeneralThermostatV2 _thermostat;

        public ZWaveThermostatCore(ZWaveDevice device)
        {
            _device = device;
            _thermostat = device.BackingObject as GeneralThermostatV2;

            SupportedModes = new List<ThermostatMode>();

            if (_thermostat == null)
            {
                return;
            }

            SetCallbacks();
        }

        private void SetCallbacks()
        {
            _thermostat.ThermostatModeChanged += (sender, args) =>
                {
                    var controlThinkMode = args.ThermostatMode;
                    var roomieMode = controlThinkMode.ToRoomieType();

                    Mode = roomieMode;

                    IEventSource source = null;
                    var @event = DeviceEvent.ThermostatModeChanged(_device, source);
                    _device.AddEvent(@event);
                };

            _thermostat.ThermostatOperatingStateChanged += (sender, args) =>
                {
                    var controlThinkAction = args.ThermostatOperatingState;
                    var roomieAction = controlThinkAction.ToRoomieType();

                    CurrentAction = roomieAction;

                    IEventSource source = null;
                    var @event = DeviceEvent.ThermostatCurrentActionChanged(_device, source);
                    _device.AddEvent(@event);
                };
        }

        public void PollCurrentAction()
        {
            var controlThinkCurrentAction = _device.DoDeviceOperation(() => _thermostat.ThermostatOperatingState);
            CurrentAction = controlThinkCurrentAction.ToRoomieType();
        }

        public void PollMode()
        {
            var controlThinkMode = _device.DoDeviceOperation(() => _thermostat.ThermostatMode);
            Mode = controlThinkMode.ToRoomieType();
        }

        public void PollSupportedModes()
        {
            var controlThinkModes = _device.DoDeviceOperation(() => _thermostat.SupportedThermostatModes);
            SupportedModes = controlThinkModes.ToRoomieType();
        }

        public void SetMode(ThermostatMode mode)
        {
            var controlThinkMode = mode.ToControlThinkType().Value;
            _device.DoDeviceOperation(() => _thermostat.ThermostatMode = controlThinkMode);
        }
    }
}
