using OpenZWaveDotNet;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries
{
    public abstract class NodeDataEntry<T> : INodeDataEntry<T>
    {
        protected readonly OpenZWaveDevice Device;
        private readonly byte _commandClass;

        protected NodeDataEntry(OpenZWaveDevice device, byte commandClass)
        {
            Device = device;
            _commandClass = commandClass;
        }

        protected OpenZWaveDeviceValue GetDataEntry()
        {
            var result = Device.GetValueByClassId(_commandClass);

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
