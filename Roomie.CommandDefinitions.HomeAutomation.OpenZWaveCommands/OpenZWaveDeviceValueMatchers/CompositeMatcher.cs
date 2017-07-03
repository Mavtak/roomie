using System.Collections.Generic;
using System.Linq;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.OpenZWaveDeviceValueMatchers
{
    public class CompositeMatcher : IOpenZWaveDeviceValueMatcher
    {
        private readonly IOpenZWaveDeviceValueMatcher[] _matchers;

        public CompositeMatcher(params IOpenZWaveDeviceValueMatcher[] matchers)
        {
            _matchers = matchers;
        }

        public bool Matches(OpenZWaveDeviceValue entry)
        {
            var result = _matchers.All(x => x.Matches(entry));

            return result;
        }

        public static IOpenZWaveDeviceValueMatcher Create(byte id, CommandClass commandClass, byte? index, byte? instance = null)
        {
            var matchers = new List<IOpenZWaveDeviceValueMatcher>
            {
                new IdMatcher(id),
                new CommandClassMatcher(commandClass)
            };

            if (index != null)
            {
                matchers.Add(new IndexMatcher(index.Value));
            }

            if (instance != null)
            {
                matchers.Add(new InstanceMatcher(instance.Value));
            }

            var result = new CompositeMatcher(matchers.ToArray());

            return result;
        }

        public override string ToString()
        {
            var result = string.Join(" and ", _matchers.Select(x => x.ToString()));

            return result;
        }
    }
}
