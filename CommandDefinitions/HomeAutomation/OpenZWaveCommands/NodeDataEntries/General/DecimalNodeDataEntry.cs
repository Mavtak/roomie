
namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General
{
    public abstract class DecimalNodeDataEntry : NodeDataEntry<decimal?>
    {
        protected DecimalNodeDataEntry(OpenZWaveDevice device, byte commandClass)
            : base(device, commandClass)
        {
        }

        public override decimal? GetValue()
        {
            var dataEntry = GetDataEntry();

            if (dataEntry == null)
            {
                return null;
            }

            decimal result;

            if (!Device.Manager.GetValueAsDecimal(dataEntry, out result))
            {
                return null;
            }

            return result;
        }

        public override void SetValue(decimal? value)
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
