using System.Collections.Generic;

namespace Roomie.Common.HomeAutomation
{
    //TODO: reconsider this class
    public class DeviceType
    {
        private static readonly Dictionary<string, DeviceType> types = new Dictionary<string, DeviceType>();

        public static readonly DeviceType Dimmable = new DeviceType("Dimmable");
        public static readonly DeviceType Switch = new DeviceType("Switch");
        public static readonly DeviceType Controller = new DeviceType("Controller");
        public static readonly DeviceType Relay = new DeviceType("Relay");
        public static readonly DeviceType MotionDetector = new DeviceType("Motion Detector");
        public static readonly DeviceType Thermostat = new DeviceType("Thermostat");
        public static readonly DeviceType Keypad = new DeviceType("Keypad");
        public static readonly DeviceType Unknown = new DeviceType("Unknown");

        public string Name { get; private set;}

        public DeviceType()
        {
            Name = Unknown.Name;
        }

        private DeviceType(string typeName)
        {
            Name = typeName;

            types.Add(Name, this);
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
            {
                return types[type];
            }

            return Unknown;
        }

        public static IEnumerable<DeviceType> Types
        {
            get
            {
                return types.Values;
            }
        }

        //TODO: move this to IDeviceActions
        public bool CanDim
        {
            get
            {
                return this == Dimmable || this == Unknown;
            }
        }

        //TODO: move this to IDeviceActions
        public bool CanControl
        {
            get
            {
                return this != Controller && this != Relay && this != MotionDetector && this != Keypad;
            }
        }

        //TODO: move this to IDeviceActions
        public bool CanPoll
        {
            get
            {
                return CanControl || Equals(Thermostat);
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
            if (that == null)
            {
                return false;
            }

            return Name == that.Name;
        }

        public static implicit operator string(DeviceType type)
        {
            return type.ToString();
        }

        public static implicit operator DeviceType(string type)
        {
            return GetTypeFromString(type);
        }
    }
}
