using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roomie.Desktop.Engine.RoomieCommandArgumentTypes
{
    public class ParameterValidationMessageHelper : IDisposable
    {
        private StringBuilder _builder;

        public ParameterValidationMessageHelper(StringBuilder builder, string parameter)
        {
            _builder = builder;

            if (parameter != null)
            {
                builder.Append(parameter);
                builder.Append(" must be ");
            }
        }

        public void Dispose()
        {
            _builder.Append(".");
        }
    }
}
