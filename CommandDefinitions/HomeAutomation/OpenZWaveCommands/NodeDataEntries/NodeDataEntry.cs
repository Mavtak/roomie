using System.Linq;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries
{
    public abstract class NodeDataEntry<T> : INodeDataEntry<T>
    {
        protected readonly OpenZWaveDevice Device;
        private readonly CommandClass _commandClass;
        protected readonly byte? _index;

        protected NodeDataEntry(OpenZWaveDevice device, CommandClass commandClass, byte? index = null)
        {
            Device = device;
            _commandClass = commandClass;
            _index = index;
        }

        protected OpenZWaveDeviceValue GetDataEntry()
        {
            var result = Device.GetValue(_commandClass, _index);

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
            if (entry.DeviceId != Device.Id)
            {
                return false;
            }

            if (entry.CommandClass != _commandClass)
            {
                return false;
            }

            if (_index != null && entry.Index != _index)
            {
                return false;
            }

            return true;
        }

        public void RefreshValue()
        {
            var dataEntry = GetDataEntry();

            dataEntry.Refresh();
        }

        public abstract bool ProcessValueChanged(OpenZWaveDeviceValue entry);

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
