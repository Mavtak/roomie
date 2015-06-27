using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;
using Roomie.Desktop.Engine.RoomieCommandArgumentTypes;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Core.BooleanOperations
{
    [BooleanParameter("Value1")]
    [BooleanParameter("Value2")]
    [StringParameter("ResultName")]
    [Description("Sets the variable in ResultName to the result of ${Value1}&&${Value2}.")]
    public class And : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var scope = context.Scope;

            bool value1 = context.ReadParameter("Value1").ToBoolean();
            bool value2 = context.ReadParameter("Value2").ToBoolean();
            string resultName = context.ReadParameter("ResultName").Value;

            bool result = value1 && value2;

            scope.Parent.Local.SetVariable(resultName, result.ToString());
        } 
    }
}
