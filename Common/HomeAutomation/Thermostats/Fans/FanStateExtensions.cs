
namespace Roomie.Common.HomeAutomation.Thermostats.Fans
{
    public static class FanStateExtensions
    {
        public static ReadOnlyFanState Copy(this IFanState state)
        {
            return ReadOnlyFanState.CopyFrom(state);
        }
    }
}
