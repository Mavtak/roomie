using System.Linq;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries
{
    public abstract class NodeDataEntry<T> : INodeDataEntry<T>
    {
        protected readonly OpenZWaveDevice Device;
        private readonly CommandClass _commandClass;
        private readonly byte? _index;

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

        public abstract T GetValue();

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
