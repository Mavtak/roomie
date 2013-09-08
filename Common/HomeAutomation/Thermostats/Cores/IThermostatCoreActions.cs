using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roomie.Common.HomeAutomation.Thermostats.Cores
{
    public interface IThermostatCoreActions
    {
        void PollCurrentAction();
        void PollMode();
        void PollSupportedModes();
        void SetMode(ThermostatMode mode);
    }
}
