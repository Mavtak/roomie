﻿using System.Collections.Generic;
using ControlThink.ZWave.Devices.Specific;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Cores;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.HomeAutomation.Thermostats.SetpointCollections;
using Roomie.Common.Temperature;

namespace Roomie.CommandDefinitions.ControlThinkCommands
{
    internal class ZWaveThermostat : IThermostat
    {
        public ITemperature Temperature { get; private set; }

        IThermostatCoreState IThermostatState.CoreState
        {
            get
            {
                return Core;
            }
        }
        IThermostatCoreActions IThermostatActions.CoreActions
        {
            get
            {
                return Core;
            }
        }
        public IThermostatCore Core { get; private set; }

        IThermostatFanState IThermostatState.FanState
        {
            get
            {
                return Fan;
            }
        }
        IThermostatFanActions IThermostatActions.FanActions
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
        ISetpointCollectionActions IThermostatActions.SetpointActions
        {
            get
            {
                return Setpoints;
            }
        }

        private ZWaveDevice _device;
        private GeneralThermostatV2 _thermostat;

        public ZWaveThermostat(ZWaveDevice device)
        {
            _device = device;
            _thermostat = device.BackingObject as GeneralThermostatV2;

            Core = new ZWaveThermostatCore(device);
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
    }
}
