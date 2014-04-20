
namespace Roomie.CommandDefinitions.OpenZWaveCommands.OpenZWaveDeviceValueMatchers
{
    public class IndexMatcher : IOpenZWaveDeviceValueMatcher
    {
        private readonly byte _index;

        public IndexMatcher(byte index)
        {
            _index = index;
        }

        public bool Matches(OpenZWaveDeviceValue entry)
        {
            var result = entry.Index == _index;

            return result;
        }

        public override string ToString()
        {
            var result = "Index = " + _index;

            return result;
        }
    }
}
