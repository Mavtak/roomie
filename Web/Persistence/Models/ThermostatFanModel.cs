﻿using System.Collections.Generic;
using Roomie.Common.HomeAutomation.Thermostats.Fans;

namespace Roomie.Web.Persistence.Models
{
    class ThermostatFanModel : IThermostatFan
    {
        public IEnumerable<ThermostatFanMode> SupportedModes { get; set; }
        public ThermostatFanMode? Mode { get; set; }
        public ThermostatFanCurrentAction? CurrentAction { get; set; }
        public void PollCurrentAction()
        {
            throw new System.NotImplementedException();
        }

        public void PollMode()
        {
            throw new System.NotImplementedException();
        }

        public void SetMode(ThermostatFanMode fanMode)
        {
            throw new System.NotImplementedException();
        }
    }
}
