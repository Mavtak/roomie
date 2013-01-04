using System;
using System.Text;

namespace Roomie.Common.Exceptions
{
    [Serializable]
    public class UnexpectedException : RoomieRuntimeException
    {
        public UnexpectedException(string message, Exception innerException = null)
            : base(message, innerException)
        { }

        public UnexpectedException(Exception innerException)
            : base(niceMessage(innerException), innerException)
        { }

        private static string niceMessage(Exception innerException)
        {
            var result = new StringBuilder();

            result.Append("Unexpected error!  ");
            result.Append(innerException.Message);

            result.Append(" (");
            result.Append(innerException.GetType());
            result.Append(")");

            //TODO: add detail

            return result.ToString();
        }
    }
}
