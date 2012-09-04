using System.Collections.Generic;
using Roomie.Common.Exceptions;
using Roomie.Common.ScriptingLanguage;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Exceptions;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Flow
{
    [Parameter("Where", "String")]
    [Parameter("NewThreadName", "String", null)]
    [Parameter("Path", "String", null)]
    public class InsertScript : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var engine = context.Engine;
            var interpreter = context.Interpreter;
            var scope = context.Scope;
            var originalCommand = context.OriginalCommand;
            var innerCommands = originalCommand.InnerCommands;

            string where = scope.GetValue("Where");
            string threadName = scope.GetValue("NewThreadName");
            string path = scope.GetValue("Path");

            IEnumerable<ScriptCommand> commandsToAdd;

            if (string.IsNullOrEmpty(path))
            {
                commandsToAdd = innerCommands;
            }
            else
            {
                commandsToAdd = RoomieScript.FromFile(path);
            }

            //TODO: detect when there are no commands?

            switch(where)
            {
                case "End":
                    interpreter.CommandQueue.Add(commandsToAdd);
                    return;
                case "Here":
                    interpreter.CommandQueue.AddBeginning(commandsToAdd);
                    return;
                case "New Thread":
                    if (string.IsNullOrEmpty(threadName))
                        throw new MissingArgumentsException("NewThreadName");
                    RoomieThread newThread = engine.Threads.CreateNewThread(threadName);
                    newThread.AddCommands(commandsToAdd);
                    return;
                default:
                    throw new RoomieRuntimeException("Unexpected value for \"Where\" (" + where + ").  Must be set to \"End\", \"Here\", or \"New Thread\"");
            }
        }
    }
}
