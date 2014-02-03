
namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General
{
    public abstract class IntNodeDataEntry : NodeDataEntry<int?>,
        IWritableNodeDataEntry<int>
    {
        protected IntNodeDataEntry(OpenZWaveDevice device, CommandClass commandClass)
            : base(device, commandClass)
        {
        }

        public override int? GetValue()
        {
            var dataEntry = GetDataEntry();

            if (dataEntry == null)
            {
                return null;
            }

            return dataEntry.IntValue;
        }

        public void SetValue(int value)
        {
            var dataEntry = GetDataEntry();

            dataEntry.SetValue(value);
        }
    }
}
