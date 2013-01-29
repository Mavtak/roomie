using System.Collections.Generic;

namespace Roomie.Desktop.Engine
{
    public class DataStore
    {
        private readonly Dictionary<object, object> _data;

        public DataStore()
        {
            _data = new Dictionary<object, object>();
        }

        public void Add(object key, object value)
        {
            lock (this)
            {
                _data.Add(key, value);
            }
        }

        public void Remove(object key)
        {
            lock (this)
            {
                _data.Remove(key);
            }
        }

        public bool Contains(object key)
        {
            return _data.ContainsKey(key);
        }

        public object this[object key]
        {
            get
            {
                return _data[key];
            }
        }

        public TValue GetAdd<TValue>(object key)
            where TValue : new()
        {
            lock (this)
            {
                if (!Contains(key))
                {
                    var toAdd = new TValue();
                    Add(key, toAdd);
                }
            }

            var value = this[key];

            return (TValue)value;
        }
    }
}
