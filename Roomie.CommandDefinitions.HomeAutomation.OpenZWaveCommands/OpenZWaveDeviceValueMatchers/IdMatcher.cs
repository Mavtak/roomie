
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

        public override string ToString()
        {
            var result = "ID = " + _deviceId;

            return result;
        }
    }
}
