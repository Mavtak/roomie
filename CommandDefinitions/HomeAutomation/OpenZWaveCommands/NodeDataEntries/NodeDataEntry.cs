using System;
using Roomie.CommandDefinitions.OpenZWaveCommands.OpenZWaveDeviceValueMatchers;
using Roomie.Common.HomeAutomation.Events;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries
{
    public abstract class NodeDataEntry<T> : INodeDataEntry<T>
    {
        protected readonly OpenZWaveDevice Device;
        protected readonly IOpenZWaveDeviceValueMatcher Matcher;

        private readonly bool _initialValueIsValid;

        public DateTime? LastUpdated { get; private set; }

        protected NodeDataEntry(OpenZWaveDevice device, CommandClass commandClass, byte? index = null, bool? initialValueIsValid = null)
            : this(device, CompositeMatcher.Create(device.Id, commandClass, index), initialValueIsValid)
        {
        }

        protected NodeDataEntry(OpenZWaveDevice device, IOpenZWaveDeviceValueMatcher matcher, bool? initialValueIsValid = null)
        {
            Device = device;
            Matcher = matcher;
            _initialValueIsValid = initialValueIsValid ?? true;
        }

        protected OpenZWaveDeviceValue GetDataEntry()
        {
            return Device.Values.Find(Matcher);
        }

        public bool HasValue()
        {
            return Device.Values.Contains(Matcher);
        }

        public abstract T GetValue();

        public bool Matches(OpenZWaveDeviceValue entry)
        {
            var result = Matcher.Matches(entry);

            return result;
        }

        public void RefreshValue()
        {
            var dataEntry = GetDataEntry();

            dataEntry.Refresh();
        }

        public bool ProcessValueUpdate(OpenZWaveDeviceValue value, ValueUpdateType updateType)
        {
            if (!Matches(value))
            {
                return false;
            }

            if (!_initialValueIsValid && updateType == ValueUpdateType.Add)
            {
                return true;
            }

            RefreshLastUpdated();

            var @event = CreateDeviceEvent();
            Device.AddEvent(@event);

            return true;
        }

        protected abstract IDeviceEvent CreateDeviceEvent();

        private void RefreshLastUpdated()
        {
            LastUpdated = DateTime.UtcNow;
        }

        public string Label
        {
            get
            {
                var dataEntry = GetDataEntry();

                if (dataEntry == null)
                {
                    return null;
                }

                return dataEntry.Label;
            }
        }

        public string Help
        {
            get
            {
                var dataEntry = GetDataEntry();

                if (dataEntry == null)
                {
                    return null;
                }

                return dataEntry.Help;
            }
        }
    }
}
