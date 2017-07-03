namespace Roomie.Common.Measurements.Temperature
{
    public interface ITemperature : IMeasurement
    {
        CelsiusTemperature Celsius { get; }
        FahrenheitTemperature Fahrenheit { get; }
        KelvinTemperature Kelvin { get; }
        ITemperature Add(double amount);
    }
}
