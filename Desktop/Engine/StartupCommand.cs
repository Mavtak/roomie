using Roomie.Common.Exceptions;

namespace Roomie.Desktop.Engine
{
    public abstract class StartupCommand : RoomieCommand
    {
        private bool hasBeenRun = false;

        protected override void Execute_Definition(RoomieCommandContext context)
        {
            if (hasBeenRun)
            {
                throw new RoomieRuntimeException("This is a startup command, and can only be run once.");
            }

            hasBeenRun = true;

            Execute_StartupDefinition(context);
        }

        protected abstract void Execute_StartupDefinition(RoomieCommandContext context);
    }
}
