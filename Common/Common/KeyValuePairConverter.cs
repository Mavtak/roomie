using System;
using System.Collections.Generic;
using System.Linq;

namespace Roomie.Common
{
    public class KeyValuePairConverter<T>
    {
        private readonly KeyValuePair<string, Func<T, string>>[] _conversions;

        public KeyValuePairConverter(params KeyValuePair<string, Func<T, string>>[] conversions)
        {
            _conversions = conversions;
        }

        public IEnumerable<string> Keys
        {
            get
            {
                var result = _conversions.Select(x => x.Key);

                return result;
            }
        }

        public IEnumerable<KeyValuePair<string, string>> Convert(T item)
        {
            foreach(var conversion in _conversions)
            {
                var key = conversion.Key;
                var value = conversion.Value(item);
                var result = new KeyValuePair<string, string>(key, value);

                yield return result;
            }
        }

        protected static KeyValuePair<string, Func<T, string>> Conversion(string key, Func<T, string> value)
        {
            return new KeyValuePair<string, Func<T, string>>(key, value);
        }
    }
}
