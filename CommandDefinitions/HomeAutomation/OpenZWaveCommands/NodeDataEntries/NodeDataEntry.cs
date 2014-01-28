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

        protected ZWValueID GetDataEntry()
        {
            var result = Device.GetValueByClassId(_commandClass);

            return result;
        }

        public abstract T GetValue();

        public abstract void SetValue(T value);

        public abstract void RefreshValue();

        public abstract bool ProcessValueChanged(ZWValueID entry);
    }
}
