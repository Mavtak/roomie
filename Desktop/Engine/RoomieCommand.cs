using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using Roomie.Common.Exceptions;
using Roomie.Common.ScriptingLanguage;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Exceptions;

namespace Roomie.Desktop.Engine
{
    public abstract class RoomieCommand : ICommandSpecification
    {
        private readonly IEnumerable<RoomieCommandArgument> _arguments;

        protected internal RoomieCommand(ICommandSpecification specificationOverrides = null)
        {
        var type = GetType();

            var specification = new CompositeCommandSpecification(
                specificationOverrides ?? new ReadOnlyCommandSpecification(),
                new AttributeBasedCommandSpecification(type),
                new NamespaceBasedCommandSpecification(type)
                );

            Name = specification.Name;
            Group = specification.Group;
            Description = specification.Description;
            Source = specification.Source;
            ExtensionName = specification.ExtensionName;
            ExtensionVersion = specification.ExtensionVersion;
            _arguments = specification.Arguments;

            if (Group == null)
            {
                throw new Exception("Command " + Name + "'s is not set");
            }

            Finalized = true;

            if (GetType().GetCustomAttributes(typeof(NotFinishedAttribute), true).Any())
            {
                Finalized = false;
            }
        }

        //TODO: make internal
        public void Execute(RoomieCommandContext context)
        {
            var interpreter = context.Interpreter;
            var scope = context.Scope;

            if (!Finalized)
            {
                throw new CommandImplementationException(this, "Can not execute a command that is not finalized.");
            }

            //print call if neccisary
            StringBuilder call = null;
            if (interpreter.Engine.PrintCommandCalls)
            {
                call = new StringBuilder();
                call.Append(FullName);
                foreach (string variable in scope.Variables)
                {
                    call.Append(" ");
                    call.Append(variable);
                    call.Append("=\"");
                    call.Append(scope.GetLiteralValue(variable));
                    call.Append("\"");
                }
            }
            bool defaultsUsed = false;

            //require specified arguments

            var missingArguments = new List<string>();
            foreach (var argument in _arguments)
            {
                if (!argument.HasDefault & !scope.VariableDefinedInThisScope(argument.Name))
                {
                    missingArguments.Add(argument.Name);
                }
            }

            if (missingArguments.Count > 0)
            {
                throw new MissingArgumentsException(missingArguments);
            }

            //here we know that all undefined arguments have available defaults.

            //fill in defaults
            foreach (var argument in _arguments)
            {
                if (!scope.VariableDefinedInThisScope(argument.Name))
                {
                    scope.DeclareVariable(argument.Name, argument.DefaultValue);

                    if (interpreter.Engine.PrintCommandCalls)
                    {
                        if (!defaultsUsed)
                        {
                            call.Append(" (with defaults");
                        }
                        call.Append(" ");
                        call.Append(argument.Name);
                        call.Append("=\"");
                        call.Append(argument.DefaultValue);
                        call.Append("\"");
                    }

                    defaultsUsed = true;
                }
            }

            if (interpreter.Engine.PrintCommandCalls && defaultsUsed)
            {
                call.Append(")");
            }

            //print out the command call
            if (interpreter.Engine.PrintCommandCalls)
            {
                interpreter.WriteEvent(call.ToString());
            }

            //now we know that all arguments are defined, but must still check the types.

            //check argument types
            var mistypedArguments = new List<string>();
            foreach (var argument in _arguments)
            {
                var value = scope.GetValue(argument.Name);
                var type = argument.Type;
                var isValid = type.Validate(value);

                if (!isValid)
                {
                    mistypedArguments.Add(argument.Name);
                    interpreter.WriteEvent(type.ValidationMessage(argument.Name));
                }
            }
            if (mistypedArguments.Count != 0)
            {
                throw new MistypedArgumentException(mistypedArguments);
            }

            Execute_Definition(context);
        }

        protected abstract void Execute_Definition(RoomieCommandContext context);

        #region names
        public string Group { get; private set; }
        public string Name { get; private set; }
        public string Source { get; private set; }
        public string ExtensionName { get; private set; }
        public Version ExtensionVersion { get; private set; }

        private string _fullName;
        public string FullName
        {
            get
            {
                if (_fullName == null)
                {
                    _fullName = Group + "." + Name;
                }

                return _fullName;
            }
        }

        public IEnumerable<RoomieCommandArgument> Arguments
        {
            get
            {
                return _arguments;
            }
        }

        #endregion

        public string Description { get; private set; }

        public bool HasDescription
        {
            get
            {
                return !String.IsNullOrEmpty(Description);
            }
        }

        public bool Finalized { get; private set; }

        public bool IsDynamic
        {
            get
            {
                return this is RoomieDynamicCommand;
            }
        }

        public void WriteToXml(XmlWriter writer)
        {
            writer.WriteStartElement("Command");
            {
                writer.WriteAttributeString("PluginName", ExtensionName);
                writer.WriteAttributeString("Group", Group);
                writer.WriteAttributeString("Name", Name);
                
                if (!String.IsNullOrEmpty(Description))
                {
                    writer.WriteAttributeString("Description", Description);
                }

                foreach (RoomieCommandArgument argument in Arguments)
                {
                    argument.WriteToXml(writer);
                }
            }
            writer.WriteEndElement();
        }

        public IScriptCommand BlankCommandCall()
        {
            return new TextScriptCommand(FullName);
        }

        public string ToConsoleFriendlyString()
        {
            var builder = new StringBuilder();

            builder.Append("Command: ");
            builder.Append(FullName);
            builder.AppendLine();

            if (!IsDynamic)
            {
                builder.Append("Source: ");
                builder.Append(Source);
                builder.AppendLine();

                builder.Append("Version: ");
                builder.Append(ExtensionVersion);
                builder.AppendLine();
            }
            else
            {
                builder.Append("Dynamic Command");
                builder.AppendLine();
            }

            builder.Append("Description: ");
            builder.Append(Description);
            builder.AppendLine();

            builder.Append("Arguments:");

            if (!Arguments.Any())
            {
                builder.Append(" (none)");
            }
            else
            {
                foreach (var argument in Arguments)
                {
                    builder.AppendLine();
                    builder.Append("\t");
                    builder.Append(argument);
                }
            }
            builder.AppendLine();

            return builder.ToString();
        }
    }
}
