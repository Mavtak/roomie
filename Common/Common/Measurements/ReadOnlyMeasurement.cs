
namespace Roomie.Common.Measurements
{
    public class ReadOnlyMeasurement : IMeasurement
    {
        public double Value { get; private set; }
        public string Units { get; private set; }

        public ReadOnlyMeasurement(double value, string units)
        {
            Value = value;
            Units = units;
        }
    }
}
