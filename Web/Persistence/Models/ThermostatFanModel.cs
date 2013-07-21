using System.Collections.Generic;
using System.Linq;
using Roomie.Common.HomeAutomation.Thermostats.Fans;

namespace Roomie.Web.Persistence.Models
{
    public class ThermostatFanModel : IThermostatFan
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

        public void PollSupportedModes()
        {
            throw new System.NotImplementedException();
        }

        public void SetMode(ThermostatFanMode fanMode)
        {
            throw new System.NotImplementedException();
        }

        public void Update(IThermostatFanState state)
        {
            Mode = state.Mode;
            CurrentAction = state.CurrentAction;
            if (state.SupportedModes != null)
            {
                SupportedModes = state.SupportedModes.ToList();
            }

        }
    }
}
