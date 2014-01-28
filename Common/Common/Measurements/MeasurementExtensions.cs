using System.Text;

namespace Roomie.Common.Measurements
{
    public static class MeasurementExtensions
    {
        public static string Format(this IMeasurement measurement)
        {
            var result = new StringBuilder();
            result.Append(measurement.Value);
            result.Append(" ");
            result.Append(measurement.Units);

            return result.ToString();
        }
    }
}
