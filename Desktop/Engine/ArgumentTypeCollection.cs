using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roomie.Desktop.Engine
{
    public class ArgumentTypeCollection : IEnumerable<IRoomieCommandArgumentType>
    {
        private readonly Dictionary<string, IRoomieCommandArgumentType> types; 

        public ArgumentTypeCollection()
        {
            types = new Dictionary<string, IRoomieCommandArgumentType>();
        }

        public void Add(IRoomieCommandArgumentType type)
        {
            lock (this)
            {
                types.Add(type.Name, type);
            }
        }

        public IRoomieCommandArgumentType this[string name]
        {
            get
            {
                return types[name];
            }
        }

        public bool Contains(string name)
        {
            return types.ContainsKey(name);
        }

        public bool Contains(IRoomieCommandArgumentType type)
        {
            var result = types.Values.Any(x => x.GetType().Equals(type.GetType()));

            return result;
        }

        #region IEnumerator

        public IEnumerator<IRoomieCommandArgumentType> GetEnumerator()
        {
            return types.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return types.Values.GetEnumerator();
        }

        #endregion
    }
}
