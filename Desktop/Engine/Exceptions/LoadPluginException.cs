using System;
using System.Reflection;
using System.Text;
using Roomie.Common.Exceptions;

namespace Roomie.Desktop.Engine.Exceptions
{
    [Serializable]
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
