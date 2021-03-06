﻿
namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General
{
    public abstract class StringNodeDataEntry : NodeDataEntry<string>,
        IWritableNodeDataEntry<string>
    {
        protected StringNodeDataEntry(OpenZWaveDevice device, CommandClass commandClass)
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

            return dataEntry.StringValue;
        }

        public void SetValue(string value)
        {
            var dataEntry = GetDataEntry();

            if (dataEntry == null)
            {
                throw new CannotSetValueException();
            }

            dataEntry.SetValue(value);
        }
    }
}
