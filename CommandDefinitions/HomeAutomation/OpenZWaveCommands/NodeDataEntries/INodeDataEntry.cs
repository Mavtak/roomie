using OpenZWaveDotNet;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries
{
    public interface INodeDataEntry<T>
    {
        T GetValue();
        void SetValue(T value);
        void RefreshValue();
        bool ProcessValueChanged(ZWValueID entry);

        string Help { get; }
        string Label { get; }
    }
}
