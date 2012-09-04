using Roomie.Common.HomeAutomation.Exceptions;
using Roomie.CommandDefinitions.HomeAutomationCommands.Exceptions;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    [NetworkParameter]
    public abstract class HomeAutomationNetworkCommand : HomeAutomationCommand
    {
        public HomeAutomationNetworkCommand()
            : base()
        { }

        protected override void Execute_HomeAutomation(HomeAutomationCommandContext context)
        {
            var engine = context.Engine;
            var interpreter = context.Interpreter;
            var scope = context.Scope;
            var originalXml = context.OriginalCommand;
            var networks = context.Networks;

            if (networks.Count == 0)
            {
                throw new HomeAutomationException("No home automation networks registered");
            }

            var networkName = scope.GetValue("Network");

            Network network;
            if (networkName == "<default>")
            {
                if(networks.Count == 1)
                {
                    network = networks.First();
                }
                else
                {
                    throw new HomeAutomationException("Network must be specified, because there are multiple registered.");
                }
            }
            else if (!networks.Contains(networkName))
            {
                throw new NetworkNotRegisteredException(networkName);
            }
            else
            {
                network = networks[networkName];
            }

            Device device = null;
            if (scope.VariableDefinedInThisScope("Device"))
            {
                string address = scope.GetValue("Device");
                
                device = networks.GetDevice(address, network);
            }


            if (scope.VariableDefinedInThisScope("AutoConnect")
                && scope.GetBoolean("AutoConnect"))
            {
                network.Connect();
            }

            try
            {
                var greaterContext = new HomeAutomationCommandContext(context)
                {
                    Network = network,
                    Device = device
                };
                Execute_HomeAutomationNetwork(greaterContext);
            }
            catch (HomeAutomationException e)
            {
                throw new HomeAutomationException(e.Message);
            }
        }

        protected abstract void Execute_HomeAutomationNetwork(HomeAutomationCommandContext context);

    }
}
