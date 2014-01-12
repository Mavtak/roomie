
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Events.Triggers;
using Roomie.Common.Triggers;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    public class RegisterPowerOnCommand : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var originalCommand = context.OriginalCommand;
            var commands = originalCommand.InnerCommands;
            var network = context.Network;
            var triggers = network.Context.Triggers;
            var history = network.Context.History;
            var threadPool = network.Context.ThreadPool;
            var device = context.Device;

            var trigger = new WhenDeviceEventHappensTrigger(device, new PoweredOn(), history.DeviceEvents);
            var action = new RunScriptTriggerAction(threadPool, commands);

            var triggerBundle = new TriggerBundle(trigger, action);

            triggers.Add(triggerBundle);
        }
    }
}