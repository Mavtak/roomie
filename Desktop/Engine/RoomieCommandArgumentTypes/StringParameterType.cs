
using System.Text;

namespace Roomie.Desktop.Engine.RoomieCommandArgumentTypes
{
    public class StringParameterType : IRoomieCommandArgumentType
    {
        public const string Key = "String";

        public string Name
        {
            get
            {
                return Key;
            }
        }

        public bool Validate(string value)
        {
            return true;
        }

        public string ValidationMessage(string parameterName = null)
        {
            var builder = new StringBuilder();

            using (new ParameterValidationMessageHelper(builder, parameterName))
            {
                builder.Append("any value that you can type");
            }

            return builder.ToString();
        }
    }
}
