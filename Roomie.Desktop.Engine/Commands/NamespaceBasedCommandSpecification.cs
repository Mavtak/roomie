using System;
using System.Collections.Generic;

namespace Roomie.Desktop.Engine.Commands
{
    public class NamespaceBasedCommandSpecification : ICommandSpecification
    {
        private readonly Type _type;

        public NamespaceBasedCommandSpecification(Type command)
        {
            _type = command;
        }

        private string _name;
        public string Name
        {
            get
            {
                if (_name == null)
                {
                    _name = CommandUtilities.GetNameFromType(_type);
                }

                return _name;
            }
        }

        private string _group;
        public string Group
        {
            get
            {
                if (_group == null)
                {
                    _group = CommandUtilities.GetGroupFromNamespace(_type);
                }

                return _group;
            }
        }

        public string Description
        {
            get
            {
                return null;
            }
        }

        private string _source;
        public string Source
        {
            get
            {
                if (_source == null)
                {
                    _source = CommandUtilities.GetExtensionSource(_type);
                }

                return _source;
            }
        }

        private string _extensionName;
        public string ExtensionName
        {
            get
            {
                if (_extensionName == null)
                {
                    _extensionName = CommandUtilities.GetExtensionNameFromNamespace(_type);
                }

                return _extensionName;
            }
        }

        private Version _extensionVersion;
        public Version ExtensionVersion
        {
            get
            {
                if (_extensionVersion == null)
                {
                    _extensionVersion = CommandUtilities.GetExtensionVersion(_type);
                }

                return _extensionVersion;
            }
        }

        public IEnumerable<RoomieCommandArgument> Arguments
        {
            get
            {
                return null;
            }
        }
    }
}