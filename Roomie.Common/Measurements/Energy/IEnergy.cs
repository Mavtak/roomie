
namespace Roomie.Common.Measurements.Energy
{
    public interface IEnergy : IMeasurement
    {
        JoulesEnergy Joules { get; }
        KilowattHoursEnergy KilowattHours { get; }
    }
}
