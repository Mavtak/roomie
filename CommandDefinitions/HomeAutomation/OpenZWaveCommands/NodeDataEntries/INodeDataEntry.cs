
namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries
{
    public interface INodeDataEntry<T>
    {
        bool HasValue();
        T GetValue();

        bool Matches(OpenZWaveDeviceValue entry);

        void RefreshValue();
        bool ProcessValueChanged(OpenZWaveDeviceValue entry);

        string Help { get; }
        string Label { get; }
    }
}
