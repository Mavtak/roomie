using System.Collections.Generic;
using System.Linq;

namespace Roomie.Common.HomeAutomation
{
    public class Location : ILocation
    {
        private string[] _parts;

        public Location()
        {
        }

        public Location(string format)
        {
            if (string.IsNullOrEmpty(format))
            {
                return;
            }

            _parts = format.Split('/');
        }

        public Location(IEnumerable<string> parts)
        {
            _parts = parts.ToArray();
        }

        public IEnumerable<string> GetParts()
        {
            return _parts ?? new string[0];
        }

        public void Update(IEnumerable<string> parts)
        {
            _parts = parts.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        }

        public bool IsSet
        {
            get
            {
                var result = _parts != null && _parts.Any();

                return result;
            }
        }

        public override int GetHashCode()
        {
            var result = string.Join("|", _parts).GetHashCode();

            return result;
        }

        public override bool Equals(object obj)
        {
            var that = obj as ILocation;

            if (obj == null)
            {
                return false;
            }

            return LocationExtensions.Equals(this, that);
        }

        public bool Equals(ILocation that)
        {
            return LocationExtensions.Equals(this, that);
        }

        public override string ToString()
        {
            return this.Format();
        }
    }
}
