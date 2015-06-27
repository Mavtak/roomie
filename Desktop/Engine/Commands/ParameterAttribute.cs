using System;

namespace Roomie.Desktop.Engine.Commands
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class ParameterAttribute : Attribute
    {
        public string Name { get; private set; }
        public IRoomieCommandArgumentType Type { get; private set; }
        public string Default { get; set; }

        private bool _hasDefault;
        public bool HasDefault
        {
            get
            {
                return _hasDefault || (Default != null);
            }
            set
            {
                _hasDefault = value;
            }
        }

        public ParameterAttribute(string name, IRoomieCommandArgumentType type)
        {
            Name = name;
            Type = type;
            Default = null;
            HasDefault = false;
        }

        public ParameterAttribute(string name, IRoomieCommandArgumentType type, string @default)
            : this(name, type)
        {
            Default = @default;
            HasDefault = true;
        }
    }
}
