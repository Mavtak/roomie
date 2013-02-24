﻿using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Attributes
{
    public class RetriesParameterAttribute : IntegerParameterAttribute
    {
        public const string Key = "Retries";

        public RetriesParameterAttribute()
            : base(Key, 3)
        {
        }
    }
}
