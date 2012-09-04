using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roomie.Desktop.Engine.Commands
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class ParameterAttribute : Attribute
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Default { get; set; }

        private bool hasDefault;
        public bool HasDefault
        {
            get
            {
                return hasDefault || (Default != null);
            }
            set
            {
                hasDefault = value;
            }
        }

        public ParameterAttribute(string name, string type)
        {
            this.Name = name;
            this.Type = type;
            this.Default = null;
            this.HasDefault = false;
        }

        public ParameterAttribute(string name, string type, string @default)
            : this(name, type)
        {
            this.Default = @default;
            this.HasDefault = true;
        }
    }
}
