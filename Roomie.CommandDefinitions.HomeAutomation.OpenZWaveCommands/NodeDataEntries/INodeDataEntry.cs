
namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries
{
    public interface INodeDataEntry<T>
    {
        bool HasValue();
        T GetValue();

        bool Matches(OpenZWaveDeviceValue entry);

        void RefreshValue();
        bool ProcessValueUpdate(OpenZWaveDeviceValue value, ValueUpdateType updateType);

        string Help { get; }
        string Label { get; }
    }
}
