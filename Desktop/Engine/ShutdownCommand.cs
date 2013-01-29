
namespace Roomie.Desktop.Engine
{
    abstract class ShutdownCommand : RoomieCommand
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {

            //TODO: some shutdown check

            Execute_ShutdownDefinition(context);
        }

        protected abstract void Execute_ShutdownDefinition(RoomieCommandContext context);
    }
}
