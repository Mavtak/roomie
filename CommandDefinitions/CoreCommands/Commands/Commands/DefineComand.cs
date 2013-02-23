using System;
using System.Collections.Generic;
using Roomie.Common.Exceptions;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.CoreCommands.Commands.Commands
{
    //TODO:
    [Parameter("Group", "String", Default = "CustomCommands")]
    [Parameter("Name", "String")]
    [Parameter("Description", "String", Default = "")]
    [Description("This command defines a new command.  The format for this needs to be documented! =P)")]
    public class Define : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var commandLibrary = context.CommandLibrary;
            var interpreter = context.Interpreter;
            var argumentTypes = context.ArgumentTypes;
            var scope = context.Scope;
            var originalCommand = context.OriginalCommand;
            var innerCommands = originalCommand.InnerCommands;

            if (originalCommand == null)
            {
                throw new RoomieRuntimeException("No custom command definition");
            }

            if (innerCommands.Select("Subcommands") == null)
            {
                throw new RoomieRuntimeException("No subcommands specified");
            }

            string group = scope.GetValue("Group");
            string name = scope.GetValue("Name");
            string description = scope.GetValue("Description");
            if (String.IsNullOrWhiteSpace(description))
            {
                description = null;
            }

            List<RoomieCommandArgument> arguments = new List<RoomieCommandArgument>();

            var argumentsDefinition = innerCommands.Select("Arguments");
            
            if (argumentsDefinition != null)
            {
                foreach (var argumentDefinition in argumentsDefinition.InnerCommands)
                {
                    if (!argumentDefinition.Parameters.ContainsParameterName("Name"))
                    {
                        throw new RoomieRuntimeException("\"Name\" not specified in an argument listing for the " + group + "." + name + " custom command.");
                    }
                    string argumentName = argumentDefinition.Parameters["Name"].Value;

                    if (!argumentDefinition.Parameters.ContainsParameterName("Type"))
                    {
                        throw new RoomieRuntimeException("Type not specified for the \"" + argumentName + "\" argumet in an argument listing for the " + group + "." + name + " custom command.");
                    }
                    string typeString = argumentDefinition.Parameters["Type"].Value;

                    if (!argumentTypes.Contains(typeString))
                    {
                        //TODO: make a "Type" type
                        throw new RoomieRuntimeException("Unexpected type encountered when parsing parameter " + argumentName + " for " + group + "." + name);
                    }

                    var type = argumentTypes[typeString];

                    if (!argumentDefinition.Parameters.ContainsParameterName("Default"))
                    {
                        var argument = new RoomieCommandArgument(
                            name: argumentName,
                            type: type
                            );
                        arguments.Add(argument);
                    }
                    else
                    {
                        string defaultValue = argumentDefinition.Parameters["Default"].Value;
                        var argument = new RoomieCommandArgument(
                            name: argumentName,
                            type: type,
                            defaultValue: defaultValue,
                            hasDefault: true
                            );
                        arguments.Add(argument);
                    }
                }
            }

            var subcommands = innerCommands.Select("Subcommands").InnerCommands;

            var dynamicCommand = new RoomieDynamicCommand(group, name, arguments, subcommands, description);

            commandLibrary.AddCommand(dynamicCommand);

        }
    }
}
