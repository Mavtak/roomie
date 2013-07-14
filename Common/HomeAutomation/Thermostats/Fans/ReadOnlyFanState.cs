using System.Collections.Generic;
using System.Linq;

namespace Roomie.Common.HomeAutomation.Thermostats.Fans
{
    public class ReadOnlyFanState : IFanState
    {
        public IEnumerable<FanMode> SupportedModes { get; private set; }
        public FanMode? Mode { get; private set; }
        public FanCurrentAction? CurrentAction { get; private set; }

        public static ReadOnlyFanState CopyFrom(IFanState state)
        {
            var result = new ReadOnlyFanState
            {
                SupportedModes = state.SupportedModes.ToList(),
                Mode = state.Mode,
                CurrentAction = state.CurrentAction
            };

            return result;
        }
    }
}
