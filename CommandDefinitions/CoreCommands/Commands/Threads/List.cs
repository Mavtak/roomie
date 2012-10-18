using System;
using System.Linq;
using Roomie.Common.TextUtilities;
using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Threads
{
    public class List : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            //TODO: make this include plugins' private threadpools.
            var interpreter = context.Interpreter;
            var threads = context.Threads;

            var headers = new string [] { "Thread Name", "Busy"};

            var maxThreadNameLength = threads.Max(thread => (thread.Name ?? string.Empty).Length);

            var tableBuilder = new TextTable(new int []
                {
                    Math.Max(maxThreadNameLength, headers[0].Length),
                    headers[1].Length
                });

            interpreter.WriteEvent(tableBuilder.StartOfTable(headers));

            foreach (var thread in threads)
            {
                interpreter.WriteEvent(tableBuilder.ContentLine(new string []
                    {
                        thread.Name,
                        thread.IsBusy ? "yes" : " - "
                    }));
            }

            interpreter.WriteEvent(tableBuilder.EndOfTable());
        }
    }
}
