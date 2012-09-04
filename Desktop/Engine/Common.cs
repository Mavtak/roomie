using System;
using System.Collections.Generic;
using System.Text;

namespace Roomie.Desktop.Engine
{
    public static class Common
    {
        public static string NiceList(List<string> items)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{");
            foreach (string item in items)
                builder.Append(item + " ");
            builder.Append("}");
            return builder.ToString();
        }

        public static Version LibraryVersion
        {
            get
            {
                return InternalLibraryVersion.Get();
            }
        }
    }
}
