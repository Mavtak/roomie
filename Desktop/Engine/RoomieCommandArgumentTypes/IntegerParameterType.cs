using System.Text;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.Desktop.Engine.RoomieCommandArgumentTypes
{
    public class IntegerParameterType : IRoomieCommandArgumentType
    {
        public const string Key = "Integer";

        public string Name
        {
            get
            {
                return Key;
            }
        }

        public bool Validate(IParameter parameter)
        {
            return parameter.IsInteger();
        }

        public string ValidationMessage(string parameterName = null)
        {
            var builder = new StringBuilder();

            using (new ParameterValidationMessageHelper(builder, parameterName))
            {
                builder.Append("a value that represents an integer, a whole number");
            }

            return builder.ToString();
        }
    }
}
