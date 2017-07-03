using System;
using System.Collections.Generic;
using System.Linq;

namespace Roomie.Desktop.Engine.Commands
{
    public class AttributeBasedCommandSpecification : ICommandSpecification
    {
        private readonly Type _type;

        public AttributeBasedCommandSpecification(Type type)
        {
            _type = type;
        }

        public string Name
        {
            get
            {
                return null;
            }
        }

        private string _group;
        public string Group
        {
            get
            {
                if (_group == null)
                {
                    _group = CommandUtilities.GetGroupFromAttribute(_type);
                }

                return _group;
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                if (_description == null)
                {
                    _description = CommandUtilities.GetDescriptionFromAttributes(_type);
                }

                return _description;
            }
        }

        public string Source
        {
            get
            {
                return null;
            }
        }

        public string ExtensionName
        {
            get
            {
                return null;
            }
        }

        public Version ExtensionVersion
        {
            get
            {
                return null;
            }
        }

        private RoomieCommandArgument[] _arguments;
        public IEnumerable<RoomieCommandArgument> Arguments
        {
            get
            {
                if (_arguments == null)
                {
                    _arguments = CommandUtilities.GetArgumentsFromAttributes(_type).ToArray();
                }

                return _arguments;
            }
        }
    }
}