﻿
namespace Roomie.Common.Measurements.Humidity
{
    public interface IHumidity : IMeasurement
    {
        RelativeHumidity Relative { get; }
    }
}
