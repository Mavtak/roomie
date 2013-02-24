using System;
using System.Text;

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

        public bool Validate(string value)
        {
            try
            {
                Convert.ToBoolean(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
