
namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General
{
    public abstract class BoolNodeDataEntry : NodeDataEntry<bool?>
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

            bool result;

            if (!Device.Manager.GetValueAsBool(dataEntry, out result))
            {
                return null;
            }

            return result;
        }

        public override void SetValue(bool? value)
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
