using Roomie.CommandDefinitions.HomeAutomationCommands.Attributes;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Events.Triggers;
using Roomie.Common.Triggers;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    [EventTypeParameter]
    public class RegisterDeviceEventTigger : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var originalCommand = context.OriginalCommand;
            var commands = originalCommand.InnerCommands;
            var device = context.Device;
            var network = device.Network;
            var triggers = network.Context.Triggers;
            var history = network.Context.History;
            var threadPool = network.Context.ThreadPool;
            var eventType = context.Scope.GetValue("EventType").ToEventType();

            var trigger = new WhenDeviceEventHappensTrigger(device, eventType, history.DeviceEvents);
            var action = new RunScriptTriggerAction(threadPool, commands);

            var triggerBundle = new TriggerBundle(trigger, action);

            triggers.Add(triggerBundle);
        }
    }
}