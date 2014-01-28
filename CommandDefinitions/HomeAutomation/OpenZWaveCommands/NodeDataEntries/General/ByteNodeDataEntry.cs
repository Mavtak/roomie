
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

            byte result;

            if (!Device.Manager.GetValueAsByte(dataEntry, out result))
            {
                return null;
            }

            return result;
        }

        public override void SetValue(byte? value)
        {
            var dataEntry = GetDataEntry();

            Device.Manager.SetValue(dataEntry, value.Value);
        }

        public override void RefreshValue()
        {
            var dataEntry = GetDataEntry();

            Device.Manager.RefreshValue(dataEntry);
        }
    }
}
