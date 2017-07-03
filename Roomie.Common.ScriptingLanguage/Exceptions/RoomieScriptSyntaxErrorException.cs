using System;
using System.Text;
using Roomie.Common.Exceptions;

namespace Roomie.Common.ScriptingLanguage.Exceptions
{
    [Serializable]
    public class RoomieScriptSyntaxErrorException : RoomieRuntimeException
    {
        public RoomieScriptSyntaxErrorException(string message = null)
            : base(message)
        { }

        public RoomieScriptSyntaxErrorException(Exception innerException)
            : base(niceMessage(innerException), innerException)
        { }
 
        private static string niceMessage(Exception innerException)
        {
            var result = new StringBuilder();

            result.Append("A syntax error is preventing me form parsing a Roomie Script. (");
            result.Append(innerException.Message);
            result.Append(")");

            return result.ToString();
        }
    }
}
