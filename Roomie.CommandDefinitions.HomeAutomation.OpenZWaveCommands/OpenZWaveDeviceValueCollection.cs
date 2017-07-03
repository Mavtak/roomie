using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Roomie.CommandDefinitions.OpenZWaveCommands.OpenZWaveDeviceValueMatchers;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveDeviceValueCollection : IEnumerable<OpenZWaveDeviceValue>
    {
        private readonly List<OpenZWaveDeviceValue> _values;

        public OpenZWaveDeviceValueCollection()
        {
            _values = new List<OpenZWaveDeviceValue>();
        }

        public bool Contains(IOpenZWaveDeviceValueMatcher matcher)
        {
            var match = Find(matcher);
            var result = match != null;

            return result;
        }
        public OpenZWaveDeviceValue Find(IOpenZWaveDeviceValueMatcher matcher)
        {
            lock (this)
            {
                return _values.FirstOrDefault(matcher.Matches);
            }
        }

        public void Add(OpenZWaveDeviceValue value)
        {
            lock (this)
            {
                _values.Add(value);
            }
        }

        public void Remove(OpenZWaveDeviceValue value)
        {
            lock (this)
            {
                var remove = _values.Match(value.DeviceId, value.CommandClass, value.Index);

                _values.Remove(remove);
            }
        }

        #region IEnumerable<OpenZWaveDeviceValue> implementation

        public IEnumerator<OpenZWaveDeviceValue> GetEnumerator()
        {
            OpenZWaveDeviceValue[] values;

            lock (this)
            {
                values = _values.ToArray();
            }

            return values.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
