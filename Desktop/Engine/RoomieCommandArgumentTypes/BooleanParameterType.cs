using System.Text;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.Desktop.Engine.RoomieCommandArgumentTypes
{
    public class BooleanParameterType : IRoomieCommandArgumentType
    {
        public const string Key = "Boolean";

        public string Name
        {
            get
            {
                return Key;
            }
        }

        public bool Validate(IParameter parameter)
        {
            return parameter.IsBoolean();
        }

        public string ValidationMessage(string parameterName = null)
        {
            var builder = new StringBuilder();

            using (new ParameterValidationMessageHelper(builder, parameterName))
            {
                builder.Append("True or False");
            }

            return builder.ToString();
        }
    }
}
