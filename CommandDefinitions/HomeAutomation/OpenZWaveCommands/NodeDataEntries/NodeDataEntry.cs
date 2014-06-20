using System;
using System.Collections.Generic;
using System.Linq;
using Roomie.CommandDefinitions.OpenZWaveCommands.OpenZWaveDeviceValueMatchers;
using Roomie.Common.HomeAutomation.Events;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries
{
    public abstract class NodeDataEntry<T> : INodeDataEntry<T>
    {
        protected readonly OpenZWaveDevice Device;
        protected readonly IOpenZWaveDeviceValueMatcher Matcher;

        public DateTime? LastUpdated { get; private set; }

        protected NodeDataEntry(OpenZWaveDevice device, CommandClass commandClass, byte? index = null)
            : this(device, CompositeMatcher.Create(device.Id, commandClass, index))
        {
        }

        protected NodeDataEntry(OpenZWaveDevice device, IOpenZWaveDeviceValueMatcher matcher)
        {
            Device = device;
            Matcher = matcher;
        }

        protected OpenZWaveDeviceValue GetDataEntry()
        {
            var result = Device.Values.FirstOrDefault(x => Matcher.Matches(x));

            return result;
        }

        public bool HasValue()
        {
            var dataEntry = GetDataEntry();

            var result = dataEntry != null;

            return result;
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
