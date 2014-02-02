﻿using System;
using Roomie.Common.Measurements.Temperature;

namespace Roomie.CommandDefinitions.OpenZWaveCommands.NodeDataEntries.General
{
    public abstract class TemperatureNodeDataEntry : NodeDataEntry<ITemperature>
    {
        protected TemperatureNodeDataEntry(OpenZWaveDevice device, byte commandClass)
            : base(device, commandClass)
        {
        }

        public override ITemperature GetValue()
        {
            var dataEntry = GetDataEntry();

            if (dataEntry == null)
            {
                return null;
            }

            var number = dataEntry.DecimalValue.Value;
            var units = dataEntry.Units;
            var result = TemperatureParser.Parse((double) number, units);

            return result;
        }

        public override void SetValue(ITemperature value)
        {
            throw new NotSupportedException();
        }
    }
}
