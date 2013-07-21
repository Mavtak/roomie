﻿using System;
using System.Collections.Generic;
using ControlThink.ZWave.Devices.Specific;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Thermostats.Fans;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    public class ZWaveThermostatFan : IThermostatFan
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

                    //TODO: raise event
                };

            _thermostat.ThermostatFanStateChanged += (sender, args) =>
                {
                    var controlThinkAction = args.ThermostatFanState;
                    var roomieAction = controlThinkAction.ToRoomieType();

                    CurrentAction = roomieAction;

                    //TODO: raise event
                };
        }

        public void PollCurrentAction()
        {
            Action operation = () =>
            {
                var controlThinkCurrentAction = _thermostat.ThermostatFanState;
                CurrentAction = controlThinkCurrentAction.ToRoomieType();
            };

            _device.DoDeviceOperation(operation);
        }

        public void PollMode()
        {
            Action operation = () =>
            {
                var controlThinkMode = _thermostat.ThermostatFanMode;
                Mode = controlThinkMode.ToRoomieType();
            };

            _device.DoDeviceOperation(operation);
        }

        public void SetMode(ThermostatFanMode fanMode)
        {
            var controlThinkFanMode = fanMode.ToControlThinkType();

            Action operation = () =>
            {
                _thermostat.ThermostatFanMode = controlThinkFanMode;
            };

            _device.DoDeviceOperation(operation);

        }
    }
}
