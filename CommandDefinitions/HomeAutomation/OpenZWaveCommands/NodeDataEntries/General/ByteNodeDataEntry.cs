
namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General
{
    public abstract class ByteNodeDataEntry : NodeDataEntry<byte?>,
        IWritableNodeDataEntry<byte>
    {
        protected ByteNodeDataEntry(OpenZWaveDevice device, CommandClass commandClass)
            : base(device, commandClass)
        {
        }

        public override byte? GetValue()
        {
            var dataEntry = GetDataEntry();

            if (dataEntry == null)
            {
                return null;
            }

            return dataEntry.ByteValue;
        }

        public void SetValue(byte value)
        {
            var dataEntry = GetDataEntry();

            if (dataEntry == null)
            {
                throw new CannotSetValueException();
            }

            dataEntry.SetValue(value);
        }
    }
}
