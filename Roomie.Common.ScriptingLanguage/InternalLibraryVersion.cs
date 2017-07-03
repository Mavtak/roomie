using System;

namespace Roomie.Common.ScriptingLanguage
{
    internal static class InternalLibraryVersion
    {
        internal static Version GetLibraryVersion()
        {
            //automatically incremented by build script
            return new Version("1.0.415.0");
        }
    }
}
