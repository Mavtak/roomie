using System;
using System.Collections.Generic;
using System.Linq;
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

            scope.PrepareForCall(this, interpreter);

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
    }
}
