
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

            return dataEntry.IntValue;
        }

        public override void SetValue(int? value)
        {
            var dataEntry = GetDataEntry();

            dataEntry.SetValue(value.Value);
        }
    }
}
