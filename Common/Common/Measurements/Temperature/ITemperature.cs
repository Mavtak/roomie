namespace Roomie.Common.Measurements.Temperature
{
    public interface ITemperature
    {
        CelsiusTemperature Celsius { get; }
        FahrenheitTemperature Fahrenheit { get; }
        KelvinTemperature Kelvin { get; }
        double Value { get; }
        ITemperature Add(double amount);
    }
}
