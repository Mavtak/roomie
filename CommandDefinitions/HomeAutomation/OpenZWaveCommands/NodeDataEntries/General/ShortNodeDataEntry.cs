
namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General
{
    public abstract class ShortNodeDataEntry : NodeDataEntry<short?>,
        IWritableNodeDataEntry<short>
    {
        protected ShortNodeDataEntry(OpenZWaveDevice device, byte commandClass)
            : base(device, commandClass)
        {
        }

        public override short? GetValue()
        {
            var dataEntry = GetDataEntry();

            if (dataEntry == null)
            {
                return null;
            }

            return dataEntry.ShortValue;
        }

        public void SetValue(short value)
        {
            var dataEntry = GetDataEntry();

            dataEntry.SetValue(value);
        }
    }
}
