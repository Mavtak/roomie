﻿using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Attributes
{
    public class PowerParameterAttribute : IntegerParameterAttribute
    {
        public PowerParameterAttribute()
            : base("Power")
        { }
    }
}
