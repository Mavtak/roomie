using Roomie.Common.Exceptions;
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

            int min = scope.GetValue("Min").ToInteger();
            int max = scope.GetValue("Max").ToInteger();
            string resultName = scope.GetValue("ResultName").Value;

            if (min > max)
                throw new RoomieRuntimeException("Min > Max");

            if(random == null)
                random = new System.Random();

            int result = random.Next(min, max);

            scope.HigherScope.ReplaceVariable(resultName, result.ToString());
        }
    }
}
