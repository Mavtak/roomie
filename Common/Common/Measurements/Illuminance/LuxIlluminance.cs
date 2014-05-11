
namespace Roomie.Common.Measurements.Illuminance
{
    public class LuxIlluminance : IIlluminance
    {
        private readonly double _value;

        public LuxIlluminance(double value)
        {
            _value = value;
        }

        public double Value
        {
            get
            {
                return _value;
            }
        }

        public string Units
        {
            get
            {
                return "Lux";
            }
        }

        public override string ToString()
        {
            return this.Format();
        }
    }
}
