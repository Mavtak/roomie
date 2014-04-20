
namespace Roomie.CommandDefinitions.OpenZWaveCommands.OpenZWaveDeviceValueMatchers
{
    public class InstanceMatcher : IOpenZWaveDeviceValueMatcher
    {
        private readonly byte _instance;

        public InstanceMatcher(byte instance)
        {
            _instance = instance;
        }

        public bool Matches(OpenZWaveDeviceValue entry)
        {
            var result = entry.Instance == _instance;

            return result;
        }

        public override string ToString()
        {
            var result = "instance = " + _instance;

            return result;
        }
    }
}
