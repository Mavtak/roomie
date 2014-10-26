using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Roomie.Common.ScriptingLanguage;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Exceptions;

namespace Roomie.Desktop.Engine
{
    public abstract class RoomieCommand : ICommandSpecification
    {
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
            Arguments = specification.Arguments;

            if (Group == null)
            {
                throw new Exception("Command " + Name + "'s is not set");
            }

            Finalized = true;

            if (GetType().GetCustomAttributes(typeof (NotFinishedAttribute), true).Any())
            {
                Finalized = false;
            }
        }

        internal void Execute(RoomieCommandContext context)
        {
            var interpreter = context.Interpreter;
            var scope = context.Scope;

            if (!Finalized)
            {
                throw new CommandImplementationException(this, "Can not execute a command that is not finalized.");
            }

            var givenValues = scope.FindGivenValues();
            var missingArguments = scope.FindMissingArguments(Arguments);

            if (missingArguments.Length > 0)
            {
                throw new MissingArgumentsException(missingArguments);
            }

            var defaultedValues = scope.ApplyDefaults(Arguments);

            if (interpreter.Engine.PrintCommandCalls)
            {
                var call = BuilCommandCall(FullName, givenValues, defaultedValues);
                interpreter.WriteEvent(call);
            }

            var mistypedArguments = scope.FindMistypedArguments(Arguments);

            if (mistypedArguments.Any())
            {
                foreach (var argument in mistypedArguments)
                {
                    interpreter.WriteEvent(argument.Type.ValidationMessage(argument.Name));
                }

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

        public IEnumerable<RoomieCommandArgument> Arguments { get; private set; }

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

        public IScriptCommand BlankCommandCall()
        {
            return new TextScriptCommand(FullName);
        }

        private static string BuilCommandCall(string fullName, KeyValuePair<string, string>[] givenValues, KeyValuePair<string, string>[] defaultedValues)
        {
            var result = new StringBuilder();

            result.Append(fullName);

            foreach (var pair in givenValues)
            {
                result.Append(" ");
                result.Append(pair.Key);
                result.Append("=\"");
                result.Append(pair.Value);
                result.Append("\"");
            }

            if (defaultedValues.Any())
            {
                result.Append("(with defaults:");

                foreach (var pair in defaultedValues)
                {
                    result.Append(" ");
                    result.Append(pair.Key);
                    result.Append("=\"");
                    result.Append(pair.Value);
                    result.Append("\"");
                }

                result.Append(")");
            }

            return result.ToString();
        }
    }
}
