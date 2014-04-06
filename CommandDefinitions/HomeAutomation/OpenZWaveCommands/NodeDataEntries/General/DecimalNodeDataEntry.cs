
namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General
{
    public abstract class DecimalNodeDataEntry : NodeDataEntry<decimal?>,
        IWritableNodeDataEntry<decimal>
    {
        protected DecimalNodeDataEntry(OpenZWaveDevice device, CommandClass commandClass, byte? index = null)
            : base(device, commandClass, index)
        {
        }

        public override decimal? GetValue()
        {
            var dataEntry = GetDataEntry();

            if (dataEntry == null)
            {
                return null;
            }

            return dataEntry.DecimalValue;
        }

        public void SetValue(decimal value)
        {
            var dataEntry = GetDataEntry();

            dataEntry.SetValue(value);
        }
    }
}
