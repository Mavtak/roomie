using System.Collections.Generic;
using System.Linq;
using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Threads
{
    [StringParameter("CurrentName", CurrentThreadName)]
    [StringParameter("NewName")]
    public class SetName : RoomieCommand
    {
        private const string CurrentThreadName = "~Current";

        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var scope = context.Scope;
            var interpreter = context.Interpreter;
            var threads = context.Threads;
            var currentThread = interpreter.ParentThread;

            var currentName = scope.ReadParameter("CurrentName").Value;
            var newName = scope.ReadParameter("NewName").Value;

            var thread = SelectThread(currentName, currentThread, threads);

            thread.Name = newName;

        }

        private static RoomieThread SelectThread(string name, RoomieThread currentThread, IEnumerable<RoomieThread> threads)
        {
            if (name == CurrentThreadName)
            {
                return currentThread;
            }

            return SelectThread(name, threads);
        }

        private static RoomieThread SelectThread(string name, IEnumerable<RoomieThread> threads)
        {
            var matches = threads
                .Where(x => string.Equals(x.Name, name))
                .ToArray();

            if (matches.Length == 0)
            {
                throw new RoomieRuntimeException("No threads were found that match the name \"" + name + "\".");
            }

            if (matches.Length > 1)
            {
                throw new RoomieRuntimeException(matches.Length + " threads were found that match the name \"" + name +
                                                 "\".");
            }

            return matches[0];
        }
    }
}
