using System;
using System.Collections.Generic;
using Roomie.Common;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.Measurements;
using Roomie.Desktop.Engine;
using Roomie.Desktop.Engine.Commands;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    [StringParameter("Filename")]
    [BooleanParameter("IncludeHeaders", true)]
    public class SaveDevicePowerEvents : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var scope = context.Scope;
            var filename = scope.ReadParameter("Filename").Value;
            var includeHeaders = scope.ReadParameter("IncludeHeaders").ToBoolean();

            var device = context.Device;
            var network = device.Network;
            var networkContext = network.Context;
            var history = networkContext.History;

            var deviceHistory = history.DeviceEvents
                .FilterEntity(device)
                .FilterType(new DevicePowerSensorValueChanged());

            var converter = new Converter();
            var writer = new CsvWriter<IDeviceEvent>(converter);

            writer.Write(filename, deviceHistory, includeHeaders);
        }

        private class Converter : KeyValuePairConverter<IDeviceEvent>
        {
            public Converter()
                : base(Conversions())
            {
            }

            private static KeyValuePair<string, Func<IDeviceEvent, string>>[] Conversions()
            {
                var result = new[]
                    {
                        Conversion("Time", x => x.TimeStamp.ToString()),
                        Conversion("Value", x => x.State.PowerSensorState.Value.Format())
                    };

                return result;
            }
        }
    }
}
