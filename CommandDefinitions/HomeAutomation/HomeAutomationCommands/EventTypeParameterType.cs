﻿using System.Text;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public class EventTypeParameterType : IRoomieCommandArgumentType
    {
        public const string Key = "EventType";

        public string Name
        {
            get { return Key; }
        }

        public bool Validate(string value)
        {
            var result = EventTypeParser.IsValid(value);

            return result;
        }

        public string ValidationMessage(string parameterName = null)
        {
            var builder = new StringBuilder();

            //TODO: list all programatically
            using (new ParameterValidationMessageHelper(builder, parameterName))
            {
                builder.Append("a value that represents an Event Type");
            }

            return builder.ToString();
        }
    }
}