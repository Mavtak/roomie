using System;
using System.Collections.Generic;
using System.Linq;
using ControlThink.ZWave.Devices.Specific;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Temperature;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    public class ZWaveThermostat : IThermostat
    {
        public ITemperature Temperature { get; private set; }
        IThermostatFanState IThermostatState.FanState
        {
            get
            {
                return Fan;
            }
        }
        public IThermostatFan Fan { get; private set; }
        public IEnumerable<ThermostatMode> SupportedModes { get; private set; }
        public ThermostatMode? Mode { get; private set; }
        public ThermostatCurrentAction? CurrentAction { get; private set; }
        private readonly ZWaveSetpointCollection _setpoints;
        public ISetpointCollection Setpoints
        {
            get
            {
                return _setpoints;
            }
        }
        ISetpointCollectionState IThermostatState.SetpointStates
        {
            get
            {
                return _setpoints;
            }
        }

        private ZWaveDevice _device;
        private GeneralThermostatV2 _thermostat;

        public ZWaveThermostat(ZWaveDevice device)
        {
            _device = device;
            _thermostat = device.BackingObject as GeneralThermostatV2;

            Fan = new ZWaveThermostatFan(device);
            SupportedModes = new List<ThermostatMode>();
            _setpoints = new ZWaveSetpointCollection(device);

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
                    //TODO raise event
                };

            _thermostat.ThermostatOperatingStateChanged += (sender, args) =>
                {
                    var controlThinkAction = args.ThermostatOperatingState;
                    var roomieAction = controlThinkAction.ToRoomieType();

                    CurrentAction = roomieAction;
                    //TODO: raise event
                };

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
            _device.DoDeviceOperation(() =>
            {
                var controlThinkTemperature = _thermostat.ThermostatTemperature;

                Temperature = controlThinkTemperature.ToRoomieType();
            });
        }

        public void PollCurrentAction()
        {
            _device.DoDeviceOperation(() =>
            {
                var controlThinkCurrentAction = _thermostat.ThermostatOperatingState;

                CurrentAction = controlThinkCurrentAction.ToRoomieType();
            });
        }

        public void PollMode()
        {
            _device.DoDeviceOperation(() =>
            {
                var controlThinkMode = _thermostat.ThermostatMode;

                Mode = controlThinkMode.ToRoomieType();
            });
        }

        public void PollSupportedModes()
        {
            _device.DoDeviceOperation(() =>
            {
                var controlThinkModes = _thermostat.SupportedThermostatModes;

                SupportedModes = controlThinkModes.ToRoomieType();
            });
        }

        public void SetMode(ThermostatMode mode)
        {
            var controlThinkMode = mode.ToControlThinkType();

            _device.DoDeviceOperation(() =>
             {
                 _thermostat.ThermostatMode = controlThinkMode;
             });
        }
    }
}
