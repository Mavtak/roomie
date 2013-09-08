using System;
using System.Collections.Generic;
using System.Linq;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.Thermostats.Cores;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    public class ThermostatCoreModel : IThermostatCore
    {
        public IEnumerable<ThermostatMode> SupportedModes { get; set; }
        public ThermostatMode? Mode { get; set; }
        public ThermostatCurrentAction? CurrentAction { get; set; }

        private DeviceModel _device;

        public ThermostatCoreModel(DeviceModel device)
        {
            _device = device;
        }

        public void PollCurrentAction()
        {
            throw new NotImplementedException();
        }

        public void PollMode()
        {
            throw new NotImplementedException();
        }

        public void PollSupportedModes()
        {
            throw new NotImplementedException();
        }

        public void SetMode(ThermostatMode mode)
        {
            _device.DoCommand("HomeAutomation.SetThermostatMode Device=\"{0}\" ThermostatMode=\"{1}\"", mode.ToString());
        }

        public void Update(IThermostatCoreState state)
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
