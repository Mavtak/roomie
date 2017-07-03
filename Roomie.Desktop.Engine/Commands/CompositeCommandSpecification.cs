using System;
using System.Collections.Generic;
using System.Linq;

namespace Roomie.Desktop.Engine.Commands
{
    public class CompositeCommandSpecification : ICommandSpecification
    {
        private readonly ICommandSpecification[] _sources;

        public CompositeCommandSpecification(params ICommandSpecification[] sources)
        {
            _sources = sources;
        }

        #region Name

        private string _name;

        public string Name
        {
            get
            {
                if (_name == null)
                {
                    RefreshName();
                }

                return _name;
            }
        }

        private void RefreshName()
        {
            var match = _sources.FirstOrDefault(x => x.Name != null);

            if (match == null)
            {
                return;
            }

            _name = match.Name;
        }

        #endregion

        #region Group

        private string _group;

        public string Group
        {
            get
            {
                if (_group == null)
                {
                    RefreshGroup();
                }

                return _group;
            }
        }

        private void RefreshGroup()
        {
            var match = _sources.FirstOrDefault(x => x.Group != null);

            if (match == null)
            {
                return;
            }

            _group = match.Group;
        }

        #endregion

        #region Description

        private string _description;

        public string Description
        {
            get
            {
                if (_description == null)
                {
                    RefreshDescription();
                }

                return _description;
            }
        }

        private void RefreshDescription()
        {
            var match = _sources.FirstOrDefault(x => x.Description != null);

            if (match == null)
            {
                return;
            }

            _description = match.Description;
        }

        #endregion

        #region Source

        private string _source;

        public string Source
        {
            get
            {
                if (_source == null)
                {
                    RefreshSource();
                }

                return _source;
            }
        }

        private void RefreshSource()
        {
            var match = _sources.FirstOrDefault(x => x.Source != null);

            if (match == null)
            {
                return;
            }

            _source = match.Source;
        }

        #endregion

        #region ExtensionName

        private string _extensionName;

        public string ExtensionName
        {
            get
            {
                if (_extensionName == null)
                {
                    RefreshExtensionName();
                }

                return _extensionName;
            }
        }

        private void RefreshExtensionName()
        {
            var match = _sources.FirstOrDefault(x => x.ExtensionName != null);

            if (match == null)
            {
                return;
            }

            _extensionName = match.ExtensionName;
        }

        #endregion

        #region ExtensionVersion

        private Version _extensionVersion;

        public Version ExtensionVersion
        {
            get
            {
                if (_extensionVersion == null)
                {
                    RefreshExtensionVersion();
                }

                return _extensionVersion;
            }
        }

        private void RefreshExtensionVersion()
        {
            var match = _sources.FirstOrDefault(x => x.ExtensionVersion != null);

            if (match == null)
            {
                return;
            }

            _extensionVersion = match.ExtensionVersion;
        }

        #endregion

        #region Arguments

        private IEnumerable<RoomieCommandArgument> _arguments;

        public IEnumerable<RoomieCommandArgument> Arguments
        {
            get
            {
                if (_arguments == null)
                {
                    RefreshArguments();
                }

                return _arguments;
            }
        }

        private void RefreshArguments()
        {
            var matches = _sources
                .Where(x => x.Arguments != null)
                .ToArray();

            if (!matches.Any())
            {
                return;
            }

            _arguments = matches.SelectMany(x => x.Arguments).ToArray();
        }

        #endregion
    }
}