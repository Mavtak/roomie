﻿
namespace Roomie.Common.Temperature
{
    public class CelsiusTemperature : ITemperature
    {
        private readonly double _value;

        public CelsiusTemperature(double value)
        {
            _value = value;
        }

        public CelsiusTemperature Celsius 
        {
            get
            {
                return this;
            }
        }

        public FahrenheitTemperature Fahrenheit
        {
            get
            {
                var result = new FahrenheitTemperature(_value * 9 / 5 + 32);
                return result;
            }
        }

        public KelvinTemperature Kelvin
        {
            get
            {
                var result = new KelvinTemperature(_value + 273.15);
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

        public override string ToString()
        {
            return _value + " Celsius";
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