
namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General
{
    public abstract class IntNodeDataEntry : NodeDataEntry<int?>
    {
        protected IntNodeDataEntry(OpenZWaveDevice device, byte commandClass)
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

            int result;

            if (!Device.Manager.GetValueAsInt(dataEntry, out result))
            {
                return null;
            }

            return result;
        }

        public override void SetValue(int? value)
        {
            var dataEntry = GetDataEntry();

            Device.Manager.SetValue(dataEntry, (float)value.Value);
        }

        public override void RefreshValue()
        {
            var dataEntry = GetDataEntry();

            Device.Manager.RefreshValue(dataEntry);
        }
    }
}
