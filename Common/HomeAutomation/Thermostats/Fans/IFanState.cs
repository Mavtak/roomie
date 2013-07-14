using System.Collections.Generic;

namespace Roomie.Common.HomeAutomation.Thermostats.Fans
{
    public interface IFanState
    {
        IEnumerable<FanMode> SupportedModes { get; }
        FanMode? Mode { get; }
        FanCurrentAction? CurrentAction { get; }
    }
}
