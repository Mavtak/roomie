
namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General
{
    public abstract class ShortNodeDataEntry : NodeDataEntry<short?>
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

            short result;

            if (!Device.Manager.GetValueAsShort(dataEntry, out result))
            {
                return null;
            }

            return result;
        }

        public override void SetValue(short? value)
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
