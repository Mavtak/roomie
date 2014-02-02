
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

            return dataEntry.ShortValue;
        }

        public override void SetValue(short? value)
        {
            var dataEntry = GetDataEntry();

            dataEntry.SetValue(value.Value);
        }
    }
}
