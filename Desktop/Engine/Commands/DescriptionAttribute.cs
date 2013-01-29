using System;

namespace Roomie.Desktop.Engine.Commands
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class DescriptionAttribute : Attribute
    {
        public string Value { get; private set; }

        public DescriptionAttribute(string value)
        {
            Value = value;
        }
    }
}
