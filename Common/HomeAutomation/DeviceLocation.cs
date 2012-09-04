using System;

namespace Roomie.Common.HomeAutomation
{
    public class DeviceLocation
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    value = null;
                }

                name = value;
            }
        }

        public bool IsSet
        {
            get
            {
                return Name != null;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
