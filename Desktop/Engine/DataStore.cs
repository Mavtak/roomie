using System.Collections.Generic;

namespace Roomie.Desktop.Engine
{
    public class DataStore
    {
        private Dictionary<object, object> data;

        public DataStore()
        {
            data = new Dictionary<object, object>();
        }

        public void Add(object key, object value)
        {
            lock (this)
            {
                data.Add(key, value);
            }
        }
        public void Remove(object key)
        {
            lock (this)
            {
                data.Remove(key);
            }
        }
        public bool Contains(object key)
        {
            return data.ContainsKey(key);
        }
        public object this[object key]
        {
            get
            {
                return data[key];
            }
        }

        public TValue GetAdd<TValue>(object key)
            where TValue : new()
        {
            lock (this)
            {
                if (!this.Contains(key))
                {
                    var toAdd = new TValue();
                    this.Add(key, toAdd);
                }
            }

            var value = this[key];

            return (TValue)value;
        }
    }
}
