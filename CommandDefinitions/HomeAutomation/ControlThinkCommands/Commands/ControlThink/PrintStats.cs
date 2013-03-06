using Roomie.Desktop.Engine;

namespace Roomie.CommandDefinitions.ZWave.ControlThinkCommands.Commands.ControlThink
{
    public class PrintStats : RoomieCommand //TODO: crease ZWaveNetworkCommand?
    {
        protected override void Execute_Definition(RoomieCommandContext context)
        {
            var interpreter = context.Interpreter;
            
            interpreter.WriteEvent("ZWave Stats:");
            interpreter.WriteEvent("--Library Version: " + Common.LibraryVersion);

            System.Reflection.Assembly sdkAssembly = System.Reflection.Assembly.GetAssembly(typeof(global::ControlThink.ZWave.ZWaveController));
            interpreter.WriteEvent("--ControlThink Z-Wave SDK Version: " + sdkAssembly.GetName().Version);
            //interpreter.WriteEvent("--Devices Registerd: " + network.Devices.Count);
        }
    }
}
