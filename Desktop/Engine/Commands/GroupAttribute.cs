using System;

namespace Roomie.Desktop.Engine.Commands
{
    public class GroupAttribute : Attribute
    {
        private readonly string[] _parts;

        public string Value
        {
            get
            {
                var result = string.Join(".", _parts);

                return result;
            }
        }

        public GroupAttribute(params string[] parts)
        {
            _parts = parts;
        }
    }
}
