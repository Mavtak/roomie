using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Attributes
{
    public class PowerParameterAttribute : ParameterAttribute
    {
        public PowerParameterAttribute()
            : base("Power", IntegerParameterType.Key)
        { }
    }
}
