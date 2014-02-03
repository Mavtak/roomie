using System.Linq;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries
{
    public abstract class NodeDataEntry<T> : INodeDataEntry<T>
    {
        protected readonly OpenZWaveDevice Device;
        private readonly CommandClass _commandClass;
        protected byte? Index { get; private set; }

        protected NodeDataEntry(OpenZWaveDevice device, CommandClass commandClass, byte? index = null)
        {
            Device = device;
            _commandClass = commandClass;
            Index = index;
        }

        protected OpenZWaveDeviceValue GetDataEntry()
        {
            var result = Device.GetValue(_commandClass, Index);

            return result;
        }

        public bool HasValue()
        {
            var dataEntry = GetDataEntry();

            var result = dataEntry != null;

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
