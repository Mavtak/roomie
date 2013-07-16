﻿using System;
using System.Collections.Generic;
using Roomie.Common.HomeAutomation.Thermostats.Fans;

namespace Roomie.Web.Persistence.Models
{
    public class FanModel : IThermostatFan
    {
        public IEnumerable<ThermostatFanMode> SupportedModes { get; set; }
        public ThermostatFanMode? Mode { get; set; }
        public ThermostatFanCurrentAction? CurrentAction { get; set; }

        public void SetMode(ThermostatFanMode fanMode)
        {
            throw new NotImplementedException();
        }
    }
}
