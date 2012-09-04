using Roomie.Common.Exceptions;

namespace Roomie.Desktop.Engine
{
    public abstract class StartupCommand : RoomieCommand
    {
        private bool hasBeenRun = false;
        public StartupCommand()
            : base()
        { }

        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var interpreter = context.Interpreter;
            var scope = context.Scope;
            var originalXml = context.OriginalCommand;

            if (hasBeenRun)
                throw new RoomieRuntimeException("This is a startup command, and can only be run once.");

            Execute_StartupDefinition(context);
        }

        protected abstract void Execute_StartupDefinition(RoomieCommandContext context);
    }
}
