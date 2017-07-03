using System;

namespace Roomie.Common.HomeAutomation.BinarySensors
{
    public interface IBinarySensorState
    {
        BinarySensorType? Type { get; }
        bool? Value { get; }
        DateTime? TimeStamp { get; }
    }
}
