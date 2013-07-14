using System;
using System.Collections.Generic;
using Roomie.Common.HomeAutomation.Thermostats.Fans;

namespace Roomie.Web.Persistence.Models
{
    public class FanModel : IFan
    {
        public IEnumerable<FanMode> SupportedModes { get; private set; }
        public FanMode? Mode { get; private set; }
        public FanCurrentAction? CurrentAction { get; private set; }

        public void SetMode(FanMode fanMode)
        {
            throw new NotImplementedException();
        }
    }
}
