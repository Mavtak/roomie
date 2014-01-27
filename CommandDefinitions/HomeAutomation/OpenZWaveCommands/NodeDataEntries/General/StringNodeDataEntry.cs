
namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General
{
    public abstract class StringNodeDataEntry : NodeDataEntry<string>
    {
        protected StringNodeDataEntry(OpenZWaveDevice device, byte commandClass)
            : base(device, commandClass)
        {
        }

        public override string GetValue()
        {
            var dataEntry = GetDataEntry();

            if (dataEntry == null)
            {
                return null;
            }

            string result;

            if (!Device.Manager.GetValueAsString(dataEntry, out result))
            {
                return null;
            }

            return result;
        }

        public override void SetValue(string value)
        {
            var dataEntry = GetDataEntry();

            Device.Manager.SetValue(dataEntry, value);
        }

        public override void RefreshValue()
        {
            var dataEntry = GetDataEntry();

            Device.Manager.RefreshValue(dataEntry);
        }
    }
}
