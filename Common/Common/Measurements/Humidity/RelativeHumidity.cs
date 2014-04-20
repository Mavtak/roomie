using Roomie.Common.Measurements.Ratio;

namespace Roomie.Common.Measurements.Humidity
{
    public class RelativeHumidity : PercentageRatio, IHumidity
    {
        public RelativeHumidity(double value)
            : base(value)
        {
        }
    }
}
