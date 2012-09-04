
namespace Roomie.Desktop.Engine
{
    abstract class ShutdownCommand : RoomieCommand
    {

        public ShutdownCommand()
            : base()
        { }

        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var interpreter = context.Interpreter;
            var scope = context.Scope;
            var originalXml = context.OriginalCommand;

            //TODO: some shutdown check

            Execute_ShutdownDefinition(context);
        }

        protected abstract void Execute_ShutdownDefinition(RoomieCommandContext context);
    }
}
