using System.Text;

namespace Roomie.Common.Measurements
{
    public static class MeasurementExtensions
    {
        public static string Format(this IMeasurement measurement, bool includeSpace = true)
        {
            var result = new StringBuilder();

            result.Append(measurement.Value);
            if (includeSpace)
            {
                result.Append(" ");
            }

            result.Append(measurement.Units);

            return result.ToString();
        }
    }
}
