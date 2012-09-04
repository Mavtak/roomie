using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;
using Roomie.Common.Exceptions;

namespace Roomie.Desktop.Engine.Exceptions
{
    public class LoadPluginException : RoomieRuntimeException
    {
        public LoadPluginException(Assembly assembly, ReflectionTypeLoadException exception)
            : base(niceMessage(assembly, exception), exception)
        { }

        private static string niceMessage(Assembly assembly, ReflectionTypeLoadException exception)
        {
            var result = new StringBuilder();

            result.Append("Failed to load ");
            result.Append(assembly.FullName);
            result.Append(". ");
            result.Append(exception.Message);

            //TODO: add detail

            return result.ToString();
        }
    }
}
