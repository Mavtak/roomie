
namespace Roomie.Common.Measurements.Ratio
{
    public class PercentageRatio : IRatio
    {
        private readonly double _value;

        public PercentageRatio(double value)
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
                return "%";
            }
        }

        public override string ToString()
        {
            return this.Format(false);
        }
    }
}
