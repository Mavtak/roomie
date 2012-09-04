using System;

namespace Roomie.CommandDefinitions.CoreCommands
{
    public static class InternalLibraryVersion
    {
        public static Version Get()
        {
            //automatically incremented by build script
            return new Version("1.0.558.0");
        }
    }
}
