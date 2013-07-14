﻿using System.Collections.Generic;
using System.Linq;
using Roomie.Common.HomeAutomation.Thermostats.Fans;
using Roomie.Common.Temperature;

namespace Roomie.Common.HomeAutomation.Thermostats
{
    public class ReadOnlyThermostatState : IThermostatState
    {
        public ITemperature Temperature { get; private set; }
        public IFanState Fan { get; private set; }
        public IEnumerable<ThermostatMode> SupportedModes { get; private set; }
        public ThermostatMode? Mode { get; private set; }
        public ThermostatCurrentAction? CurrentAction { get; private set; }
        public ISetpointCollection SetPoints { get; private set; }

        public static ReadOnlyThermostatState CopyFrom(IThermostatState source)
        {
            var result = new ReadOnlyThermostatState
            {
                Temperature = source.Temperature,
                Fan = source.Fan.Copy(),
                SupportedModes = source.SupportedModes.ToList(),
                Mode = source.Mode,
                CurrentAction = source.CurrentAction,
                SetPoints = source.SetPoints.Copy()
            };

            return result;
        }
    }
}
