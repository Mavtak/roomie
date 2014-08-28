﻿using System;
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
    public abstract class RoomieCommand
    {
        private readonly List<RoomieCommandArgument> _arguments;

        protected RoomieCommand()
            : this(group: null, name: null, source: null, version: null, arguments: new List<RoomieCommandArgument>(), finalized: null)
        {
        }

        protected internal RoomieCommand(string group, string name, string source, Version version, List<RoomieCommandArgument> arguments, bool? finalized, string description = null)
        {
            _cachedGroup = group;
            _cachedName = name;
            _cashedSource = source;
            _cachedPluginVersion = version;
            _arguments = arguments;

            _arguments.AddRange(this.GetArgumentsFromAttributes());

            if (description != null)
            {
                Description = description;
            }
            else
            {
                Description = this.GetDescriptionFromAttributes();
            }

            Finalized = true;

            if (GetType().GetCustomAttributes(typeof(NotFinishedAttribute), true).Any())
            {
                finalized = false;
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
        private string _cachedGroup;
        public string Group
        {
            get
            {
                if (_cachedGroup == null)
                {
                    _cachedGroup = this.GetGroupFromNamespace();
                }

                return _cachedGroup;
            }
        }

        private string _cachedName;
        public string Name
        {
            get
            {
                if (_cachedName == null)
                {
                    _cachedName = this.GetNameFromType();
                }

                return _cachedName;
            }
        }

        private string _cashedSource;
        public string Source
        {
            get
            {
                if (_cashedSource == null)
                {
                    _cashedSource = Assembly.GetAssembly(GetType()).CodeBase;
                }

                return _cashedSource;
            }
        }

        private string _cachedPluginName;
        public string ExtensionName
        {
            get
            {
                if (_cachedPluginName == null)
                {
                    var temp = GetType().Namespace;
                    temp = temp.Substring(0, temp.LastIndexOf(".Commands."));

                    _cachedPluginName = temp;
                }

                return _cachedPluginName;
            }
        }

        private Version _cachedPluginVersion;
        //private static string[] possibleVersionClassNames = { "LibraryVersion", "Common", "Version", "InternalLibraryVersion" };
        //private static string[] possibleVersionMethodNames = { "Get", "GetVersion", "GetLibraryVersion" };
        public Version ExtensionVersion
        {
            get
            {
                if(_cachedPluginVersion == null)
                {
                    //TODO: make this more dynamic

                    var assembly = Assembly.GetAssembly(GetType());

                    //cachedPluginVersion = (Version)RoomieUtils.TryInvoke(assembly, possibleVersionClassNames, possibleVersionMethodNames, new object[] { }, typeof(Version));

                    _cachedPluginVersion = assembly.GetName().Version;
                }

                return _cachedPluginVersion;
            }
        }

        private string _cashedFullName;
        public string FullName
        {
            get
            {
                if (_cashedFullName == null)
                {
                    _cashedFullName = Group + "." + Name;
                }

                return _cashedFullName;
            }
        }

        public ICollection<RoomieCommandArgument> Arguments
        {
            get
            {
                return new List<RoomieCommandArgument>(_arguments);
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

            if (Arguments.Count == 0)
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
