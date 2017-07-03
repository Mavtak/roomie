using System.Collections.Generic;
using System.Linq;
using Roomie.Common.ScriptingLanguage;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public class DeviceEventAction
    {
        public List<DeviceEventType> EventTypes { get; private set; }
        public ScriptCommandList Commands { get; private set; }

        public DeviceEventAction(IEnumerable<DeviceEventType> eventTypes, ScriptCommandList commands)
        {
            EventTypes = eventTypes.ToList();
            Commands = commands;
        }

        public DeviceEventAction(DeviceEventType eventType, ScriptCommandList commands)
            : this(new[] {eventType}, commands)
        {
        }

        public bool Matches(DeviceEventType eventType)
        {
            var result = EventTypes.Contains(eventType);

            return result;
        }
    }
}
