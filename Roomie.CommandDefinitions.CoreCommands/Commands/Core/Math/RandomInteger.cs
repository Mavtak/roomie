﻿using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core.Math
{
    [IntegerParameter("Min")]
    [IntegerParameter("Max")]
    [StringParameter("ResultName")]
    [Description("Returns an Integer (in the variable specified by ResultName) between Min and Max (inclusive)")]
    public class RandomInteger : RoomieCommand
    {
        System.Random random = null;
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var scope = context.Scope;

            int min = context.ReadParameter("Min").ToInteger();
            int max = context.ReadParameter("Max").ToInteger();
            string resultName = context.ReadParameter("ResultName").Value;

            if (min > max)
                throw new RoomieRuntimeException("Min > Max");

            if(random == null)
                random = new System.Random();

            int result = random.Next(min, max);

            scope.Parent.SetVariable(resultName, result.ToString());
        }
    }
}
