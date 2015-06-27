using System.Collections.Generic;
using Roomie.Common.Exceptions;
using Roomie.Common.ScriptingLanguage;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Exceptions;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Flow
{
    [StringParameter("Where")]
    [StringParameter("NewThreadName", null)]
    [StringParameter("Path", null)]
    public class InsertScript : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var threads = context.Threads;
            var interpreter = context.Interpreter;
            var scope = context.Scope;
            var originalCommand = context.OriginalCommand;
            var innerCommands = originalCommand.InnerCommands;

            string where = context.ReadParameter("Where").Value;
            string threadName = context.ReadParameter("NewThreadName").Value;
            string path = context.ReadParameter("Path").Value;

            IEnumerable<IScriptCommand> commandsToAdd;

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
                    RoomieThread newThread = threads.CreateNewThread(threadName, scope);
                    newThread.AddCommands(commandsToAdd);
                    return;
                default:
                    throw new RoomieRuntimeException("Unexpected value for \"Where\" (" + where + ").  Must be set to \"End\", \"Here\", or \"New Thread\"");
            }
        }
    }
}
