
namespace Roomie.CommandDefinitions.OpenZWaveCommands.OpenZWaveDeviceValueMatchers
{
    public class IdMatcher : IOpenZWaveDeviceValueMatcher
    {
        private readonly byte _deviceId;

        public IdMatcher(byte deviceId)
        {
            _deviceId = deviceId;
        }

        public bool Matches(OpenZWaveDeviceValue entry)
        {
            var result = entry.DeviceId == _deviceId;

            return result;
        }
    }
}
