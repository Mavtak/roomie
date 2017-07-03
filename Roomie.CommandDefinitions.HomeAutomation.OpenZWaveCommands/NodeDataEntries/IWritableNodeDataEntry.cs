
namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries
{
    public interface IWritableNodeDataEntry<T>
    {
        void SetValue(T value);
    }
}
