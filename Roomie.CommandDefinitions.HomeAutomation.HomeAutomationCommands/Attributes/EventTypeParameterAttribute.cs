using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Attributes
{
    public class EventTypeParameterAttribute : ParameterAttribute
    {
        public EventTypeParameterAttribute()
            : base("EventType", new EventTypeParameterType())
        {
        }
    }
}