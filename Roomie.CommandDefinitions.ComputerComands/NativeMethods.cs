using System.Runtime.InteropServices;

namespace Roomie.CommandDefinitions.ComputerCommands
{
    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
        internal static extern void LockWorkStation();
    }
}
