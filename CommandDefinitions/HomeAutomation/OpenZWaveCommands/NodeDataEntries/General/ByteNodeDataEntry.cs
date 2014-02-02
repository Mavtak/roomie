
namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General
{
    public abstract class ByteNodeDataEntry : NodeDataEntry<byte?>
    {
        protected ByteNodeDataEntry(OpenZWaveDevice device, byte commandClass)
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

        public override void SetValue(byte? value)
        {
            var dataEntry = GetDataEntry();

            dataEntry.SetValue(value.Value);
        }
    }
}
