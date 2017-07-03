
namespace Roomie.Common.Measurements.Power
{
    public class WattsPower : IPower
    {
        private readonly double _value;

        public WattsPower(double value)
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
                return "Watts";
            }
        }

        public override string ToString()
        {
            return this.Format();
        }
    }
}
