
namespace Roomie.CommandDefinitions.OpenZWaveCommands.OpenZWaveDeviceValueMatchers
{
    public interface IOpenZWaveDeviceValueMatcher
    {
        bool Matches(OpenZWaveDeviceValue entry);
    }
}
