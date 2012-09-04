using System;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Flow
{
    [NotFinished]
    [Parameter("Value", "Boolean")]
    [Description("Switches execution based on the value of %{Value}.  "
                + " The inner XML can either be the code to be executed on true or two nodes named \"true\" and \"false\", each with their respective conditional code.")]
    public class If : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            throw new NotImplementedException();
            //var interpreter = context.Interpreter;
            //var scope = context.Scope;
            //var originalXml = context.OriginalCommand;

            //bool value = scope.GetBoolean("Value");

            //XmlNode nodeToExecute;
            //if (value)
            //    nodeToExecute = originalXml.SelectSingleNode("True");
            //else
            //    nodeToExecute = originalXml.SelectSingleNode("False");

            //if (nodeToExecute == null)
            //    return;

            //List<XmlNode> commands = new List<XmlNode>(nodeToExecute.ChildNodes.Count);
            //foreach (XmlNode commandNode in nodeToExecute.ChildNodes)
            //    commands.Add(commandNode);

            //RoomieCommandInterpreter subInterpreter = interpreter.GetSubinterpreter();
            //subInterpreter.AddCommands(commands);
            //bool success = subInterpreter.ProcessQueue();

            //if (!success)
            //{
            //    lock (interpreter)
            //    {
            //        interpreter.WriteEvent("Breaking if");
            //        interpreter.ClearCommandQueue();
            //    }
            //}
        }
    }
}
