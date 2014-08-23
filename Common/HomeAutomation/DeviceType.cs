using System;
using System.Collections.Generic;
using System.Linq;

namespace Roomie.Common.HomeAutomation
{
    //TODO: unit test
    //TODO: make more efficient
    public class DeviceType
    {
        private static readonly Dictionary<string, DeviceType> types = new Dictionary<string, DeviceType>();

        public static readonly DeviceType MultilevelSwitch = new DeviceType("Multilevel Switch", "Dimmable");
        public static readonly DeviceType BinarySwitch = new DeviceType("Binary Switch", "Switch");
        public static readonly DeviceType Controller = new DeviceType("Controller");
        public static readonly DeviceType Relay = new DeviceType("Relay");
        public static readonly DeviceType BinarySensor = new DeviceType("Binary Sensor", "Door Sensor", "Window Sensor", "Motion Detector");
        public static readonly DeviceType MultilevelSensor = new DeviceType("Multilevel Sensor", "Temperature Sensor", "Humidity Sensor", "Light Sensor");
        public static readonly DeviceType Thermostat = new DeviceType("Thermostat");
        public static readonly DeviceType Keypad = new DeviceType("Keypad");
        public static readonly DeviceType Lock = new DeviceType("Lock");
        public static readonly DeviceType Unknown = new DeviceType();

        private const string UnknownName = "Unknown";

        public string Name
        {
            get
            {
                var staticType = GetTypeFromString(Names.FirstOrDefault());

                var result = staticType.Names.FirstOrDefault() ?? UnknownName;

                return result;
            }
            protected set
            {
                // used by Entity Framework
                _names.AddFirst(value);
            }
        }

        public IEnumerable<string> Names
        {
            get
            {
                return _names;
            }
        }

        private readonly LinkedList<string> _names; 

        public DeviceType()
        {
            _names = new LinkedList<string>();
        }

        private DeviceType(params string [] names)
            :this()
        {
            foreach (var name in names.Where(x => x != UnknownName))
            {
                _names.AddLast(name);
            }

            types.Add(_names.First(), this);
        }

        public static bool IsValidType(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                return false;
            }

            return types.ContainsKey(type);
        }

        public static DeviceType GetTypeFromString(string typeName)
        {
            foreach (var type in types.Values)
            {
                if (type.Names.Any(x => x.Equals(typeName, StringComparison.InvariantCultureIgnoreCase)))
                {
                    return type;
                }
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
                return this == MultilevelSwitch || this == Unknown;
            }
        }

        //TODO: move this to IDeviceActions
        public bool CanControl
        {
            get
            {
                return this != Controller && this != Relay && this != BinarySensor && this != Keypad;
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
            var one = GetTypeFromString(Name);
            var two = GetTypeFromString(value);

            return one == two;
        }

        public bool Equals(DeviceType that)
        {
            if (that == null)
            {
                return false;
            }

            var one = GetTypeFromString(Name);
            var two = GetTypeFromString(that.Name);

            return one == two;
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
