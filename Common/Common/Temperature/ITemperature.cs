namespace Roomie.Common.Temperature
{
    public interface ITemperature
    {
        CelsiusTemperature Celsius { get; }
        FahrenheitTemperature Fahrenheit { get; }
        KelvinTemperature Kelvin { get; }
        double Value { get; }
    }
}
