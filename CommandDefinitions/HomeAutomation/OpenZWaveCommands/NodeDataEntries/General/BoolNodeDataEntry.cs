
namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General
{
    public abstract class BoolNodeDataEntry : NodeDataEntry<bool?>,
        IWritableNodeDataEntry<bool>
    {
        protected BoolNodeDataEntry(OpenZWaveDevice device, byte commandClass)
            : base(device, commandClass)
        {
        }

        public override bool? GetValue()
        {
            var dataEntry = GetDataEntry();

            if (dataEntry == null)
            {
                return null;
            }

            return dataEntry.BooleanValue;
        }

        public void SetValue(bool value)
        {
            var dataEntry = GetDataEntry();

            dataEntry.SetValue(value);
        }
    }
}
