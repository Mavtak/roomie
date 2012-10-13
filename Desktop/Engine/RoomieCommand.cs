using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;
using Roomie.Common.Exceptions;
using Roomie.Common.ScriptingLanguage;
using Roomie.Desktop.Engine.Exceptions;
using System.Linq;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.Desktop.Engine
{
    public abstract class RoomieCommand
    {
        private List<RoomieCommandArgument> arguments;

        public RoomieCommand()
            : this((string)null, (string)null, (string)null, (Version)null, new List<RoomieCommandArgument>(), (bool?) null)
        { }

        /// <summary>
        /// Only Commands in this library can use this constructor.  It is for the CustomCommand command.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="name"></param>
        /// <param name="requiredArguments"></param>
        /// <param name="argumentDefaults"></param>
        /// <param name="argumentTypes"></param>
        protected internal RoomieCommand(string group, string name, string source, Version version, List<RoomieCommandArgument> arguments, bool? finalized, string description = null)
        {
            this.cachedGroup = group;
            this.cachedName = name;
            this.cashedSource = source;
            this.cachedPluginVersion = version;
            this.arguments = arguments;

            //Reflect on the parameters
            foreach (ParameterAttribute parameter in this.GetType().GetCustomAttributes(typeof(ParameterAttribute), true))
            {
                var argument = new RoomieCommandArgument(
                    name: parameter.Name,
                    type: parameter.Type,
                    defaultValue: parameter.Default,
                    hasDefault: parameter.HasDefault
                    );
                arguments.Add(argument);
            }

            if (description != null)
            {
                this.Description = description;
            }
            else
            {
                //Reflect on the description
                var descriptionAttribute = this.GetType().GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
                if (descriptionAttribute != null)
                {
                    this.Description = descriptionAttribute.Value;
                }
            }

            Finalized = true;

            if (this.GetType().GetCustomAttributes(typeof(NotFinishedAttribute), true).Any())
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
                throw new CommandImplementationException(this, "Can not execute a command that is not finalized.");

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

            List<string> missingArguments = new List<string>();
            foreach (RoomieCommandArgument argument in arguments)
            {
                if (!argument.HasDefault & !scope.VariableDefinedInThisScope(argument.Name))
                    missingArguments.Add(argument.Name);
            }
            if (missingArguments.Count > 0)
                throw new MissingArgumentsException(missingArguments);

            //here we know that all undefined arguments have available defaults.

            //fill in defaults
            foreach (RoomieCommandArgument argument in arguments)
                if (!scope.VariableDefinedInThisScope(argument.Name))
                {
                    scope.DeclareVariable(argument.Name, argument.DefaultValue);

                    if (interpreter.Engine.PrintCommandCalls)
                    {
                        if (!defaultsUsed)
                            call.Append(" (with defaults");
                        call.Append(" ");
                        call.Append(argument.Name);
                        call.Append("=\"");
                        call.Append(argument.DefaultValue);
                        call.Append("\"");
                    }

                    defaultsUsed = true;
                }
            if (interpreter.Engine.PrintCommandCalls && defaultsUsed)
                call.Append(")");

            //print out the command call
            if (interpreter.Engine.PrintCommandCalls)
                interpreter.WriteEvent(call.ToString());

            //now we know that all arguments are defined, but must still check the types.

            //check argument types
            List<string> mistypedArguments = new List<string>();
            foreach (RoomieCommandArgument argument in arguments)
            {
                string value = scope.GetValue(argument.Name);
                switch (argument.Type)
                {
                    case "String":
                        //no problem
                        break;

                    case "Boolean":
                        try
                        {
                            Convert.ToBoolean(value);
                        }
                        catch
                        {
                            mistypedArguments.Add(argument.Name);
                        }
                        break;

                    case "Integer":
                        try
                        {
                            System.Convert.ToInt32(value);
                        }
                        catch
                        {
                            mistypedArguments.Add(argument.Name);
                        }
                        break;

                    case "Byte":
                        try
                        {
                            System.Convert.ToByte(value);
                        }
                        catch
                        {
                            mistypedArguments.Add(argument.Name);
                        }
                        break;

                    case "TimeSpan":
                        if(!TimeUtils.IsTimeSpan(value))
                            mistypedArguments.Add(argument.Name);
                        break;

                    case "DateTime":
                        if (!TimeUtils.IsDateTime(value))
                            mistypedArguments.Add(argument.Name);
                        break;
                    default:
                        throw new RoomieRuntimeException("Unknown argument type \"" + argument.Type + "\".");
                }
                if (mistypedArguments.Count != 0)
                    throw new MistypedArgumentException(mistypedArguments);
            }

            Execute_Definition(context);
        }

        protected abstract void Execute_Definition(RoomieCommandContext context);

        #region names
        private string cachedGroup = null;
        public string Group
        {
            get
            {
                if (cachedGroup == null)
                {
                    try
                    {
                        string result = GetType().Namespace;
                        result = result.Substring(result.LastIndexOf(".Commands.") + ".Commands.".Length);
                        if (string.IsNullOrEmpty(result))
                            throw new Exception("just goin' to the catch block"); //TODO: review this.  It seems awful
                        cachedGroup = result;
                    }
                    catch
                    {
                        //TODO: What kind of exception should be thrown here?
                        throw new Exception("Command " + Name + "'s namespace is not in the proper form");
                    }
                }

                return cachedGroup;
            }
        }

        private string cachedName = null;
        public string Name
        {
            get
            {
                if (cachedName == null)
                {
                    cachedName = GetType().Name;
                }

                return cachedName;
            }
        }

        private string cashedSource;
        public string Source
        {
            get
            {
                if (cashedSource == null)
                {
                    cashedSource = System.Reflection.Assembly.GetAssembly(GetType()).CodeBase;
                }

                return cashedSource;
            }
        }

        private string cachedPluginName = null;
        public string ExtensionName
        {
            get
            {
                if (cachedPluginName == null)
                {
                    var temp = this.GetType().Namespace;
                    temp = temp.Substring(0, temp.LastIndexOf(".Commands."));

                    cachedPluginName = temp;
                }
                return cachedPluginName;
            }
        }

        private Version cachedPluginVersion;
        //private static string[] possibleVersionClassNames = { "LibraryVersion", "Common", "Version", "InternalLibraryVersion" };
        //private static string[] possibleVersionMethodNames = { "Get", "GetVersion", "GetLibraryVersion" };
        public Version ExtensionVersion
        {
            get
            {
                if(cachedPluginVersion == null)
                {
                    //TODO: make this more dynamic

                    Assembly assembly = Assembly.GetAssembly(GetType());

                    //cachedPluginVersion = (Version)RoomieUtils.TryInvoke(assembly, possibleVersionClassNames, possibleVersionMethodNames, new object[] { }, typeof(Version));

                    cachedPluginVersion = assembly.GetName().Version;
                }

                return cachedPluginVersion;
            }
        }

        private string cashedFullName = null;
        public string FullName
        {
            get
            {
                if (cashedFullName == null)
                    cashedFullName = Group + "." + Name;
                return cashedFullName;
            }
        }

        public ICollection<RoomieCommandArgument> Arguments
        {
            get
            {
                return new List<RoomieCommandArgument>(arguments);
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
                    writer.WriteAttributeString("Description", Description);
                foreach (RoomieCommandArgument argument in Arguments)
                    argument.WriteToXml(writer);
            }
            writer.WriteEndElement();
        }

        public ScriptCommand BlankCommandCall()
        {
            return new ScriptCommand(this.FullName);
        }

        public string ToConsoleFriendlyString()
        {
            var builder = new StringBuilder();

            builder.Append("Command: ");
            builder.Append(this.FullName);
            builder.AppendLine();

            if (!this.IsDynamic)
            {
                builder.Append("Source: ");
                builder.Append(this.Source);
                builder.AppendLine();

                builder.Append("Version: ");
                builder.Append(this.ExtensionVersion);
                builder.AppendLine();
            }
            else
            {
                builder.Append("Dynamic Command");
                builder.AppendLine();
            }

            builder.Append("Description: ");
            builder.Append(this.Description);
            builder.AppendLine();

            builder.Append("Arguments:");

            if (this.Arguments.Count == 0)
            {
                builder.Append(" (none)");
            }
            else
            {
                foreach (var argument in this.Arguments)
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
