using System.Linq;
using Roomie.CommandDefinitions.HomeAutomationCommands.Exceptions;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Exceptions;
using Roomie.Common.Triggers;
using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    [NetworkParameter]
    public abstract class HomeAutomationNetworkCommand : HomeAutomationCommand
    {
        public HomeAutomationNetworkCommand()
            : base()
        { }

        protected override void Execute_HomeAutomationDefinition(HomeAutomationCommandContext context)
        {
            var interpreter = context.Interpreter;
            var scope = context.Scope;
            var originalXml = context.OriginalCommand;
            var networks = context.Networks;

            if (networks.Count == 0)
            {
                throw new HomeAutomationException("No home automation networks registered");
            }

            var networkName = scope.ReadParameter("Network").Value;

            Network network;
            if (networkName == "<default>")
            {
                if(networks.Count() == 1)
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
                network = networks.GetNetwork(networkName);
            }

            try
            {
                var greaterContext = new HomeAutomationCommandContext(context)
                {
                    Network = network
                };
                Execute_HomeAutomationNetworkDefinition(greaterContext);
            }
            catch (HomeAutomationException e)
            {
                throw new HomeAutomationException(e.Message);
            }
        }

        protected abstract void Execute_HomeAutomationNetworkDefinition(HomeAutomationCommandContext context);

    }
}
