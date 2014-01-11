using System.Collections.Generic;

namespace Roomie.Common.HomeAutomation
{
    //TODO: reconsider this class
    public class DeviceType
    {
        private static Dictionary<string, DeviceType> types = new Dictionary<string, DeviceType>();

        public static readonly DeviceType Dimmable = new DeviceType("Dimmable", true, true);
        public static readonly DeviceType Switch = new DeviceType("Switch", false, true);
        public static readonly DeviceType Controller = new DeviceType("Controller", false, false);
        public static readonly DeviceType Relay = new DeviceType("Relay", false, false);
        public static readonly DeviceType MotionDetector = new DeviceType("Motion Detector", false, false);
        public static readonly DeviceType Thermostat = new DeviceType("Thermostat", false, false);
        public static readonly DeviceType Keypad = new DeviceType("Keypad", false, false);
        public static readonly DeviceType Unknown = new DeviceType("Unknown", true, true);

        public string Name { get; private set;}

        public DeviceType()
        {
            Name = Unknown.Name;
        }
        private DeviceType(string typeName, bool canDim, bool canControl)
        {
            this.Name = typeName;

            types.Add(this.Name, this);
        }

        public static bool IsValidType(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                return false;
            }

            return types.ContainsKey(type);
        }

        public static DeviceType GetTypeFromString(string type)
        {
            if (IsValidType(type))
                return types[type];
            return Unknown;
        }

        public static IEnumerable<DeviceType> Types
        {
            get
            {
                return types.Values;
            }
        }

        public bool CanDim
        {
            get
            {
                return Name.Equals("Dimmable") || Name.Equals("Unknown");
            }
        }
        public bool CanControl
        {
            get
            {
                return !Name.Equals("Controller") && !Name.Equals("Relay") && !Name.Equals("Motion Detector") && !Name.Equals("Keypad");
            }
        }
        public bool CanPoll
        {
            get
            {
                return CanControl || Equals(Thermostat);
            }
        }
        public bool IsController
        {
            get
            {
                return this.Equals(Controller);
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(string value)
        {
            return Equals(GetTypeFromString(value));
        }
        public bool Equals(DeviceType that)
        {
            return that != null
                     && this.Name.Equals(that.Name);
        }

        public static implicit operator string(DeviceType type)
        {
            return type.ToString();
        }
        public static implicit operator DeviceType(string type)
        {
            return DeviceType.GetTypeFromString(type);
        }
    }
}
