using System;

namespace Roomie.Common.ScriptingLanguage
{
    public static class ScriptCommandParameterExtensions
    {
        public static int IntegerValue(this ScriptCommandParameter parameter)
        {
            return Convert.ToInt32(parameter.Value);
        }
    }
}
