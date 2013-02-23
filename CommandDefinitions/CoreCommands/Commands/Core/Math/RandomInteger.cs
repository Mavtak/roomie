using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core.Math
{
    [Parameter("Min", "Integer")]
    [Parameter("Max", "Integer")]
    [Parameter("ResultName", StringParameterType.Key)]
    [Description("Returns an Integer (in the variable specified by ResultName) between Min and Max (inclusive)")]
    public class RandomInteger : RoomieCommand
    {
        System.Random random = null;
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var scope = context.Scope;

            int min = scope.GetInteger("Min");
            int max = scope.GetInteger("Max");
            string resultName = scope.GetValue("ResultName");

            if (min > max)
                throw new RoomieRuntimeException("Min > Max");

            if(random == null)
                random = new System.Random();

            int result = random.Next(min, max);

            scope.HigherScope.ReplaceVariable(resultName, result.ToString());
        }
    }
}
