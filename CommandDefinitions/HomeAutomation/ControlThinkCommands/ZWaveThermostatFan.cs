using System.Collections.Generic;
using ControlThink.ZWave.Devices.Specific;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Thermostats.Fans;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    internal class ZWaveThermostatFan : IThermostatFan
    {
        public IEnumerable<ThermostatFanMode> SupportedModes { get; internal set; }
        public ThermostatFanMode? Mode { get; internal set; }
        public ThermostatFanCurrentAction? CurrentAction { get; internal set; }

        private ZWaveDevice _device;
        private GeneralThermostatV2 _thermostat;

        public ZWaveThermostatFan(ZWaveDevice device)
        {
            _device = device;
            _thermostat = device.BackingObject as GeneralThermostatV2;

            SupportedModes = new List<ThermostatFanMode>();

            if (_thermostat == null)
            {
                return;
            }

            SetCallbacks();
        }

        private void SetCallbacks()
        {
            _thermostat.ThermostatFanModeChanged += (sender, args) =>
                {
                    var controlThinkMode = args.ThermostatFanMode;
                    var roomieMode = controlThinkMode.ToRoomieType();

                    Mode = roomieMode;

                    IEventSource source = null;
                    var @event = DeviceEvent.ThermostatFanModeChanged(_device, source);
                    _device.AddEvent(@event);
                };

            _thermostat.ThermostatFanStateChanged += (sender, args) =>
                {
                    var controlThinkAction = args.ThermostatFanState;
                    var roomieAction = controlThinkAction.ToRoomieType();

                    CurrentAction = roomieAction;

                    IEventSource source = null;
                    var @event = DeviceEvent.ThermostatFanCurrentActionChanged(_device, source);
                    _device.AddEvent(@event);
                };
        }

        public void PollCurrentAction()
        {
            var controlThinkCurrentAction = _device.DoDeviceOperation(() => _thermostat.ThermostatFanState);
            CurrentAction = controlThinkCurrentAction.ToRoomieType();
        }

        public void PollMode()
        {
            var controlThinkMode = _device.DoDeviceOperation(() => _thermostat.ThermostatFanMode);
            Mode = controlThinkMode.ToRoomieType();
        }

        public void PollSupportedModes()
        {
            var controlThinkModes = _device.DoDeviceOperation(() => _thermostat.SupportedThermostatFanModes);
            SupportedModes = controlThinkModes.ToRoomieType();
        }

        public void SetMode(ThermostatFanMode fanMode)
        {
            var controlThinkFanMode = fanMode.ToControlThinkType();
            _device.DoDeviceOperation(() => _thermostat.ThermostatFanMode = controlThinkFanMode);
        }
    }
}
