using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.HomeAutomationCommands
{
    public abstract class HomeAutomationCommand : RoomieCommand
    {
        public HomeAutomationCommand()
            : base()
        { }

        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var greaterContext = new HomeAutomationCommandContext(context);

            Execute_HomeAutomation(greaterContext);
        }

        protected abstract void Execute_HomeAutomation(HomeAutomationCommandContext context);
    }
}
