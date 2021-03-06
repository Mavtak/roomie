﻿using Roomie.Common.HomeAutomation.Events.Triggers;
using Roomie.Common.Triggers;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.CommandDefinitions.HomeAutomationCommands.Commands.HomeAutomation
{
    [StringParameter("CurrentAction")]
    public class RegisterCurrentActionChangedCommand : HomeAutomationSingleDeviceCommand
    {
        protected override void Execture_HomeAutomationSingleDeviceDefinition(HomeAutomationSingleDeviceContext context)
        {
            var currentAction = context.ReadParameter("CurrentAction").Value;
            var originalCommand = context.OriginalCommand;
            var commands = originalCommand.InnerCommands;
            var device = context.Device;
            var network = device.Network;
            var triggers = network.Context.Triggers;
            var history = network.Context.History;
            var threadPool = network.Context.ThreadPool;

            var trigger = new WhenTheCurrentActionChangesTrigger(device, currentAction, history.DeviceEvents);
            var action = new RunScriptTriggerAction(threadPool, commands);

            var triggerBundle = new TriggerBundle(trigger, action);

            triggers.Add(triggerBundle);
        }
    }
}