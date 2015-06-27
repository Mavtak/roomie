using System;
using System.Text;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.Desktop.Engine.RoomieCommandArgumentTypes
{
    public class IntegerRangeParameterType : IRoomieCommandArgumentType
    {
        public const string Key = "IntegerRange";

        private int? _min;
        private int? _max;

        public string Name
        {
            get
            {
                return Key;
            }
        }

        public IntegerRangeParameterType(int? min, int? max)
        {
            _min = min;
            _max = max;
        }

        public bool Validate(IParameter parameter)
        {
            try
            {
                var number = Convert.ToInt64(parameter.Value);

                if (number < _min)
                {
                    return false;
                }

                if (number > _max)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public string ValidationMessage(string parameterName = null)
        {
            var builder = new StringBuilder();

            using (new ParameterValidationMessageHelper(builder, parameterName))
            {
                builder.Append("a whole number from ");
                builder.Append(_min);
                builder.Append(" to ");
                builder.Append(_max);
            }

            return builder.ToString();
        }
    }
}
