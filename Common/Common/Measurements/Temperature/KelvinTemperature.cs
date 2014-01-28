namespace Roomie.Common.Temperature
{
    public class KelvinTemperature : ITemperature
    {
        private readonly double _value;

        public KelvinTemperature(double value)
        {
            _value = value;
        }

        public CelsiusTemperature Celsius
        {
            get
            {
                var result = new CelsiusTemperature(_value - 273.15);
                return result;
            }
        }

        public FahrenheitTemperature Fahrenheit
        {
            get
            {
                var result = new FahrenheitTemperature((_value - 273.15) * 9 / 5 + 32);
                return result;
            }
        }

        public KelvinTemperature Kelvin
        {
            get
            {
                return this;
            }
        }

        public double Value
        {
            get
            {
                return _value;
            }
        }

        public ITemperature Add(double amount)
        {
            var result = new FahrenheitTemperature(Value + amount);

            return result;
        }

        public override string ToString()
        {
            return _value + " Kelvin";
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