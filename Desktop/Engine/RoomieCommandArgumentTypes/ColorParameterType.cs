using System.Text;
using Roomie.Common.Color;

namespace Roomie.Desktop.Engine.RoomieCommandArgumentTypes
{
    public class ColorParameterType : IRoomieCommandArgumentType
    {
        public const string Key = "Color";

        public string Name
        {
            get
            {
                return Key;
            }
        }

        public bool Validate(string value)
        {
            return ColorParser.IsValid(value);
        }

        public string ValidationMessage(string parameterName = null)
        {
            var builder = new StringBuilder();

            using (new ParameterValidationMessageHelper(builder, parameterName))
            {
                builder.Append("a value that represents a color, like #FF0000 or Red");
            }

            return builder.ToString();
        }
    }
}
