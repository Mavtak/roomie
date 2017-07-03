
namespace Roomie.Common.Measurements.Temperature
{
    public class FahrenheitTemperature : ITemperature
    {
        private readonly double _value;

        public FahrenheitTemperature(double value)
        {
            _value = value;
        }

        public CelsiusTemperature Celsius 
        {
            get
            {
                var result = new CelsiusTemperature((_value - 32) * 5 / 9);
                return result;
            }
        }

        public FahrenheitTemperature Fahrenheit
        {
            get
            {
                return this;
            }
        }

        public KelvinTemperature Kelvin
        {
            get
            {
                var result = new KelvinTemperature((_value - 32) * 5 / 9 + 273.15);
                return result;
            }
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
                return "Fahrenheit";
            }
        }

        public ITemperature Add(double amount)
        {
            var result = new FahrenheitTemperature(Value + amount);

            return result;
        }

        public override string ToString()
        {
            return this.Format();
        }

        public override bool Equals(object obj)
        {
            return Utilities.EqualsHelper(this, obj);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
