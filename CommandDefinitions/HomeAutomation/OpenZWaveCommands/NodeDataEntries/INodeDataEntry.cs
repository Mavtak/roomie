
namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries
{
    public interface INodeDataEntry<T>
    {
        T GetValue();

        void RefreshValue();
        bool ProcessValueChanged(OpenZWaveDeviceValue entry);

        string Help { get; }
        string Label { get; }
    }
}
